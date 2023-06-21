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
        countryList: [],
        propertyId: '',
        startDate: '',
        rentCycle: '',
        leaseType: '',
        endDate: '',
        totalAmount: 0,
        memo: '',
        rentAccountId: '',
        ledgerAccountId: '',
        rentChargeList: [],
        tenantOrCosignersList: [],
        documentList: [],
        securityDepositDueDate: '',
        securityDepositAmount: null,
        propertyLeaseId: ''
         
    },
    validations() {

        return {

            propertyId: {
                required
            },
            startDate: {
                required
            },
            endDate: {
                required
            },
            rentCycle: {
                required
            },
            ledgerAccountId: {
                required
            },
            leaseType: {
                required
            },
             
            totalAmount: {
                required
            },
        }


    },
    methods: {
        
        getProperties() {

            axios.get('/Rentals/GetAvailableProperties').then(response => {

                this.propertyList = response.data;

                $("#propertyId").ejDropDownList({
                    dataSource: response.data,
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
      
        getLedgerAccounts() {

            axios.get('/LedgerSetups/GetLedgerAccounts').then(response => {

                this.ledgerAccountList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get ledger accounts.');

            });
        },

        getCountries() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.countryList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get countries.');

            });
        },

        resetForm() {
           
            this.propertyId = '';
            this.startDate = '';
            this.rentCycle = '';
            this.leaseType = '';
            this.endDate = '';
            this.totalAmount = '';
            this.memo = 0;
            this.rentAccountId = '';
            this.ledgerAccountId = '';
            this.rentChargeList = [];
            this.tenantOrCosignersList = [];
            this.documentList = [];
            this.securityDepositDueDate = '';
            this.securityDepositAmount = null;
            this.propertyLeaseId = '';
            
            this.$v.$reset();

        },

        addRentCharge() {

            this.rentChargeList.push({ 'id': this.uuidv4() , 'amount': 0, 'memo': '', 'rentAccountId': '', 'nextDueDate': '' });
        },

        addTenantOrCoSigner() {

            this.tenantOrCosignersList.push({
                'id': this.uuidv4(),
                'tenantIdentifier': Math.floor((Math.random() * 999999) + 10000),
                'firstName': '',
                'lastName': '',
                'primaryPhoneNo': '',
                'secondaryPhoneNo': '',
                'primaryEmail': '',
                'secondaryEmail': '',
                'dateOfBirth': this.formatDate(new Date()),
                'taxIdentificationNo': '',
                'comments': '',
                'emergencyContactName': '',
                'emergencyContactPhoneNo': '',
                'emergencyContactEmail' : '',
                'emergencyContactRelationshipToTenant': '',
                'address': '',
                'countryId': '',
                'regionOrState': '',
                'city': '',
                'isCoSigner': false
            });
        },

        removeTenantOrCoSigner(id) {

            this.tenantOrCosignersList.splice(id, 1);
        },

        removeRentCharge(id) {

            this.rentChargeList.splice(id, 1);
        },

        leaseProperty() {

            this.$v.$touch();

            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }

                }

                return;
            }  

            if (this.tenantOrCosignersList.length == 0)
            {
                this.toastHandler('warning', 'One or more tenants or co-signers is required.');

            }

            let payload = {};

            payload.propertyId = this.propertyId;
            let propertyInfo = this.getPropertyDetails(this.propertyId);

            if (propertyInfo) {
                payload.propertyName = propertyInfo.name
            }
            payload.startDate = this.startDate;
            payload.rentCycle = this.rentCycle;
            payload.memo = this.memo;
            payload.ledgerAccountId = !this.ledgerAccountId ? this.emptyGuid : this.ledgerAccountId;
            payload.leaseType = this.leaseType;
            payload.endDate = this.endDate;
            payload.totalAmount = parseFloat(this.totalAmount);

            this.rentChargeList.forEach((v) => {

                v.amount = parseFloat(v.amount);

            });

            payload.rentCharges = this.rentChargeList;

            this.tenantOrCosignersList.forEach((v) => {

                v.id = this.uuidv4();

                if (v.countryId) {

                    let countryInfo = this.getCountryDetails(v.countryId);

                    if (countryInfo) {

                        v.countryName = countryInfo.name;
                    }
                }
                else {

                    v.countryId = this.emptyGuid;

                }

                v.leasedPropertyId = this.propertyId;

                if (propertyInfo) {

                    v.leasedPropertyName = propertyInfo.name;

                }

                v.leasedPropertyUnitId = this.emptyGuid;

            });

            payload.tenantOrCosigners = this.tenantOrCosignersList;
            payload.securityDepositDueDate = !this.securityDepositDueDate ? this.formatDate(new Date()) : this.securityDepositDueDate;
            payload.securityDepositAmount = this.securityDepositAmount ? parseFloat(this.securityDepositAmount) : 0;
 

            if (this.propertyLeaseId) {

                payload.id = this.propertyLeaseId;
                payload.tenantId = this.tenantId;
                
                axios.post('/Rentals/UpdatePropertyLease', payload).then(response => {

                    this.toastHandler('', 'Property lease info updated.');


                }).catch(err => {

                    this.toastHandler('error', 'Unable to update property info');

                });


            } else {

                axios.post('/Rentals/CreatePropertyLease', payload).then(response => {

                    this.toastHandler('', 'Property lease info saved.');

                    this.resetForm();

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save property lease info.');

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
 

        getCountryDetails(id) {

            return this.countryList.find(a => a.id === id);
        },

        getPropertyDetails(id) {

            return this.propertyList.find(a => a.id === id);
        },
        

        uuidv4() {
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
                .replace(/[xy]/g, function (c) {
                    const r = Math.random() * 16 | 0,
                        v = c == 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
        },

        initControls() {
             
            $(() => {

                $("#startDate").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        if (args.value) {

                            this.startDate = args.value;
                        }
                    }

                });

            });

            $(() => {

                $("#endDate").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        if (args.value) {

                            this.endDate = args.value;
                        }
                    }

                });

            });

            $(() => {

                $("#securityDepositDueDate").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        if (args.value) {

                            this.securityDepositDueDate = args.value;
                        }
                    }

                });

            });
        },

        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");
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

       

    },
    created() {

        this.getCountries();

        this.getProperties();

        this.initControls();

        this.getLedgerAccounts();
        
    },
});