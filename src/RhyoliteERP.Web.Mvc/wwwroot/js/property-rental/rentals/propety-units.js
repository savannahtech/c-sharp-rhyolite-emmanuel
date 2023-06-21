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
        propertyUnitList: [],
        noOfUnitsPerProperty: 1,
        autoCreateUnits: false,
        unitNo: '',
        size: '',
        rooms: '',
        amenities: '',
        marketRent: '',
        marketRent: null,
        address: '',
        baths: '',
        propertyId: '',
        propertyUnitId: '',
        description: '',
        amenitiesList: [],
        searchFilter: {

            propertyId: '',
            propertyUnitId: ''
        },
        amenityPlaceHolderSuggestions: ['Air Condition', 'Cable Ready', 'Heating', 'High Speed Internet']
    },
    validations() {

        return {

            propertyId: {
                required
            },


        }


    },
    methods: {
        
        getProperties() {

            axios.get('/Rentals/GetProperties').then(response => {

                this.propertyList = response.data;

                $("#propertyIdQuery").ejDropDownList({
                    dataSource: response.data.sort((a, b) => a.name.localeCompare(b.name)),
                    watermarkText: "Select Property",
                    width: "100%",
                    fields: { id: "id", text: "name", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.searchFilter.propertyId = args.value;
                    }

                });

                $("#propertyId").ejDropDownList({
                    dataSource: response.data.sort((a, b) => a.name.localeCompare(b.name)),
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

        getPropertyUnits() {

            if (this.searchFilter.propertyId) {

                axios.get(`/Rentals/GetPropertyUnits?propertyId=${this.searchFilter.propertyId}`).then(response => {

                    this.propertyUnitList = response.data;

                    console.log(this.propertyUnitList);

                    $("#propertyUnitId").ejDropDownList({
                        dataSource: response.data.sort((a, b) => a.unitNo.localeCompare(b.unitNo)),
                        watermarkText: "Select Unit",
                        width: "100%",
                        fields: { id: "id", text: "unitNo", value: "id" },
                        enableFilterSearch: true,
                        change: (args) => {

                            this.searchFilter.propertyUnitId = args.value;
                        }

                    });

                    }).catch(err => {

                });

            }

            
        },
      
        getLedgerAccounts() {

            axios.get('/LedgerSetups/GetLedgerAccounts').then(response => {

                this.ledgerAccountList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get ledger accounts.');

            });
        },

        addAmenities() {

            this.amenitiesList.push({name: ''});
        },

        removeAmenity(id) {

            this.amenitiesList.splice(id, 1);
        },

        resetForm() {

            this.propertyId = '';
            this.unitNo = '';
            this.rooms = '';
            this.description = '';
            this.address = '';
            this.baths = '';
            this.size = '';
            this.amenitiesList = [];
           
        },

        savePropertyUnit() {

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

            this.isLoading = true;

            let payload = {};

            payload.propertyId = this.propertyId;
            payload.unitNo = this.unitNo;
            payload.rooms = this.rooms;
            payload.description = this.description;
            payload.marketRent = this.marketRent;
            payload.address = this.address;
            payload.baths = this.baths;
            payload.size = this.size;

            this.amenitiesList.forEach((v) => {

                v.id = this.uuidv4();
            });

            payload.unitAmenities = this.amenitiesList;
            payload.autoCreateUnits = this.autoCreateUnits;
            payload.noOfUnitsPerProperty = this.noOfUnitsPerProperty;
         

            if (this.propertyUnitId) {

                payload.id = this.propertyUnitId;
                payload.tenantId = this.tenantId;
                
                axios.post('/Rentals/UpdateProperty', payload).then(response => {

                    this.toastHandler('', 'Property unit info updated.');
                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to update property unit info');
                    this.isLoading = false;

                });


            } else {

                axios.post('/Rentals/CreatePropertyUnit', payload).then(response => {

                    this.toastHandler('', 'Property unit info saved.');

                    this.isLoading = false;

                    //navigate to listings page

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save property unit info.');
                    this.isLoading = false;

                });
            }

           
        },

     
        generatePropertyUnitNo() {

            axios.get('/SharedResource/GetSystemNumbers').then(response => {

                if (response.data.length) {

                    for (i = 0; i < response.data.length; i++) {

                        let itemName = response.data[i].itemName;

                        if (itemName == 'PropertyUnitNo') {

                            this.currentSystemNumber = response.data[i];

                            this.unitNo = this.currentSystemNumber.prefix + this.currentSystemNumber.lastNo + this.currentSystemNumber.suffix;

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


        setCurrentPropertyUnit() {

            let propertyUnitInfo = this.getPropertyUnitDetails(this.searchFilter.propertyUnitId);


            this.propertyUnitId = propertyUnitInfo.id;
            this.propertyId = propertyUnitInfo.propertyId;
            this.unitNo = propertyUnitInfo.unitNo;
            this.rooms = propertyUnitInfo.rooms;
            this.description = propertyUnitInfo.description;
            this.marketRent = propertyUnitInfo.marketRent;
            this.address = propertyUnitInfo.address;
            this.baths = propertyUnitInfo.baths;
            this.size = propertyUnitInfo.size;
            this.amenitiesList = propertyUnitInfo.unitAmenities;

        },

        getPropertyUnitDetails(id) {

            return this.propertyUnitList.find(a => a.id === id);
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


            //$("#propertyUnitId").ejDropDownList({
            //    dataSource: [] ,
            //    watermarkText: "Select Unit",
            //    width: "100%",
            //    fields: { id: "id", text: "unitNo", value: "id" },
            //    enableFilterSearch: true,
            //    change: (args) => {

            //        this.propertyUnitId = args.value;
            //    }

            //});


            $(() => {

                $("#startTime").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        if (args.value) {

                            this.startTime = args.value;
                        }
                    }

                });

            });

            $(() => {

                $("#endTime").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        if (args.value) {

                            this.endTime = args.value;
                        }
                    }

                });

            });
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

        'searchFilter.propertyId': {
             handler(val) {
                
                this.getPropertyUnits();
            },
            deep: true
        },

        'searchFilter.propertyUnitId': {
            handler(val) {

                this.setCurrentPropertyUnit();
            },
            deep: true
        },
    },
    created() {

        this.getProperties();

        this.initControls();

        this.getLedgerAccounts();

        this.generatePropertyUnitNo();

    },
});