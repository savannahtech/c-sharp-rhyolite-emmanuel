Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        ledgerAccountList: [],
        propertyList: [],
        propertyId: '',
        name: '',
        propertyTypeId: '',
        address: '',
        regionOrState: '',
        propertyManager: '',
        propertyManagerPhoneNo: '',
        ledgerAccountId: '',
        propertyIdentifier: '',
        propertyGroupId: '',
        countryId: '',
        countryName: '',
        city: '',
        propertyManagerEmail: '',
        propertyReserve: null,
        propertyTypeList: [],
        ledgerAccountList: [],
        propertyGroupList: [],
        countryList: [],
        yearBuilt: '',
        gpsLongitude: '',
        gpsLongitude: '',
        gpsLatitude: '',
        imageList: [],
         
    },
    validations() {

        return {

            name: {
                required
            },

            propertyManager: {
                required
            },

            propertyManagerPhoneNo: {
                required
            },

            propertyIdentifier: {
                required
            },
            
        }


    },
    methods: {
        
        getProperties() {

            axios.get('/Rentals/GetProperties').then(response => {

                this.propertyList = response.data;

                $("#propertyId").ejDropDownList({
                    dataSource: response.data ,
                    watermarkText: "Select Property",
                    width: "100%",
                    fields: { id: "id", text: "name", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.propertyId = args.value;
                    }

                });


            }).catch(err => {

            });
        },

        getPropertyGroups() {

            axios.get('/RentalSetups/GetPropertyGroups').then(response => {
 
                this.propertyGroupList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get property groups.');

            });
        },

        getPropertyTypes() {

            axios.get('/RentalSetups/GetPropertyTypes').then(response => {

                this.propertyTypeList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get property types.');

            });
        },

        getLedgerAccounts() {

            axios.get('/LedgerSetups/GetLedgerAccounts').then(response => {

                this.ledgerAccountList = response.data;

            }).catch(err => {


            });
        },
        
        getCountries() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.countryList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get nationalities.');

            });
        },
        
        resetForm() {

            this.name = '';
            this.propertyTypeId = '';
            this.address = '';
            this.regionOrState = '';
            this.propertyManager = '';
            this.propertyManagerPhoneNo = '';
            this.ledgerAccountId = '';
            this.propertyIdentifier = '';
            this.propertyGroupId = '';
            this.countryId = '';
            this.countryName = '';
            this.city = '';
            this.propertyManagerEmail = '';
            this.propertyReserve = null;

            this.$v.$reset();
            
        },

        registerProperty() {

            this.$v.$touch();

            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }
                    1
                }

                return;
            }  

            this.isLoading = true;

            let payload = {};

            payload.propertyIdentifier = this.propertyIdentifier;
            payload.name = this.name;
            payload.propertyTypeId = this.propertyTypeId;
            payload.address = this.address;
            payload.regionOrState = this.regionOrState;
            payload.propertyManager = this.propertyManager;
            payload.propertyManagerPhoneNo = this.propertyManagerPhoneNo;
            payload.propertyManagerEmail = this.propertyManagerEmail;
            payload.propertyGroupId = this.propertyGroupId;
            payload.countryId = this.countryId;
            payload.propertyReserve = !this.propertyReserve ? 0 : parseFloat(this.propertyReserve);
            payload.city = this.city;
            payload.ledgerAccountId = !this.ledgerAccountId ? this.emptyGuid : this.ledgerAccountId ;
 

            if (!this.propertyTypeId) {
 
                payload.propertyTypeId = this.emptyGuid;
            }

            if (!this.propertyGroupId) {

                payload.propertyGroupId = this.emptyGuid;

            }

            if (!this.countryId) {

                payload.ledgerAccountId = this.emptyGuid;

            }

            if (!this.ledgerAccountId) {

                payload.ledgerAccountId = this.emptyGuid;

            }
             


            if (this.propertyId) {

                payload.id = this.propertyId;
                payload.tenantId = this.tenantId;
                
                axios.post('/Rentals/UpdateProperty', payload).then(response => {

                    this.toastHandler('', 'Property info updated.');

                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to update property info');

                    this.isLoading = false;

                    this.getProperties();

                });


            } else {

                axios.post('/Rentals/CreateProperty', payload).then(response => {

                    this.toastHandler('', 'Property info saved.');

                    this.isLoading = false;

                    //redirect to property list page.

                    this.getProperties();

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save property info.');
                    this.isLoading = false;

                });
            }

           
        },

        generatePropertyIdentifier() {

            axios.get('/SharedResource/GetSystemNumbers').then(response => {

                if (response.data.length) {

                    for (i = 0; i < response.data.length; i++) {

                        let itemName = response.data[i].itemName;

                        if (itemName == 'PropertyID') {

                            this.currentSystemNumber = response.data[i];

                            this.propertyIdentifier = this.currentSystemNumber.prefix + this.currentSystemNumber.lastNo + this.currentSystemNumber.suffix;

                            break;
                        }

                    }
                }
                else {

                    this.propertyIdentifier = Math.floor(Math.random() * 99999) + 100;
                }


            }).catch(err => {


            });


        },

        setCurrentProperty() {

            let propertyInfo = this.getPropertiesDetails(this.propertyId);

            this.propertyIdentifier = propertyInfo.propertyIdentifier;
            this.name = propertyInfo.name;
            this.propertyTypeId = propertyInfo.propertyTypeId;
            this.address = propertyInfo.address;
            this.regionOrState = propertyInfo.regionOrState;
            this.propertyManager = propertyInfo.propertyManager;
            this.propertyManagerPhoneNo = propertyInfo.propertyManagerPhoneNo;
            this.ledgerAccountId = propertyInfo.ledgerAccountId;
            this.propertyGroupId = propertyInfo.propertyGroupId;
            this.countryId = propertyInfo.countryId;

            let countryInfo = this.getCountryDetails(this.countryId);
            if (countryInfo != null) {
                this.countryName = countryInfo.name;

            }


            this.city = propertyInfo.city;
            this.propertyManagerEmail = propertyInfo.propertyManagerEmail;
            this.propertyReserve = propertyInfo.propertyReserve;



        },

        getCountryDetails(id) {

            return this.countryList.find(a => a.id === id);
        },

        getPropertiesDetails(id) {

            return this.propertyList.find(a => a.id === id);
        },
 
        toastHandler(type, message = 'An unexpected error occurred.') {

            //display alert with 
            toastr.options = {
                closeButton: true,
                progressBar: true,
                showMethod: 'fadeIn',
                hideMethod: 'fadeOut',
                timeOut: 10000,
            };

            switch (type) {

                case 'warning':
                    toastr.warning('', message);
                    break;
                case 'error':
                    toastr.error('', message);
                    break;
                default:
                    toastr.success('', message);
                    break;
            }

        },

        
    },
    watch: {

        propertyId(val) {

            if (val) {

                this.setCurrentProperty();

            }

        },
        countryId(val) {

            if (val) {

                let countryInfo = this.getCountryDetails(this.countryId);

                if (countryInfo != null)
                {
                    this.countryName = countryInfo.name.trim().toLowerCase();

                }

            }

        }

    },
    created() {

        this.getProperties();

        this.getPropertyGroups();

        this.getPropertyTypes();

        this.getLedgerAccounts();

        this.getCountries();

        this.generatePropertyIdentifier();

    },
});