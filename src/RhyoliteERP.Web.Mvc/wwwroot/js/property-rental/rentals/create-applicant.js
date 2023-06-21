Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        propertyList: [],
        propertyUnitList: [],
        propertyId: '',
        propertyUnitId: '',
        firstName: '',
        lastName: '',
        email: '',
        primaryPhoneNo: '',
        secondaryPhoneNo: '',
        isPropertyUnitControlRendered: '',
        emptyGuid: '00000000-0000-0000-0000-000000000000'

    },
    validations() {

        return {

            firstName: {
                required
            },

            lastName: {
                required
            },

            primaryPhoneNo: {
                required
            },

            propertyUnitId: {
                required
            },
        }


    },
    methods: {

        getProperties() {

            axios.get('/Rentals/GetProperties').then(response => {

                this.propertyList = response.data;

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

            if (this.propertyId) {

                axios.get(`/Rentals/GetPropertyUnits?propertyId=${this.propertyId}`).then(response => {

                    this.propertyUnitList = response.data;

                    $("#propertyUnitId").ejDropDownList({
                        dataSource: response.data.sort((a, b) => a.unitNo.localeCompare(b.unitNo)),
                        watermarkText: "Select Unit",
                        width: "100%",
                        fields: { id: "id", text: "unitNo", value: "id" },
                        enableFilterSearch: true,
                        change: (args) => {

                            this.propertyUnitId = args.value;
                        }

                    });

                    this.isPropertyUnitControlRendered = true;

                }).catch(err => {

                });

            }


        },
     
        saveApplicant() {

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


            let payload = {};

            this.isLoading = true;

            if (this.propertyId == '') {

                this.propertyId = this.emptyGuid;
            }

            if (this.propertyUnitId == '') {

                this.propertyUnitId = this.emptyGuid;
            }

            payload.firstName = this.firstName;
            payload.lastName = this.lastName;
            payload.email = this.email;
            payload.primaryPhoneNo = this.primaryPhoneNo;
            payload.secondaryPhoneNo = this.secondaryPhoneNo;
            payload.propertyUnitId = this.propertyUnitId;
            payload.propertyId = this.propertyId;


            let propertyInfo = this.getPropertyDetails(this.propertyId);

            if (propertyInfo) {

                payload.propertyName = propertyInfo.name;

            }

            let propertyUnitInfo = this.getPropertyUnitDetails(this.propertyUnitId);

            if (propertyUnitInfo) {

                payload.propertyUnitName = propertyUnitInfo.unitNo;
            }
         
            axios.post('/Rentals/CreateLeaseApplicants', payload).then((response) => {

                this.isLoading = false;

                this.toastHandler('', 'Applicant info saved.');

                setTimeout(() => window.location.href = '/Rentals/Applicants', 2000);

            }).catch((error) => {

                this.isLoading = false;

                this.toastHandler('error', 'Unable to save applicant info.');

            });

        },
         
        getPropertyDetails(id) {

            return this.propertyList.find(a => a.id === id);
        },

        getPropertyUnitDetails(id) {

            return this.propertyUnitList.find(a => a.id === id);
        },

        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

        },

        resetForm() {

            this.studentIdentifier = null;
            this.firstName = null;
           
            this.religionId = null;
            this.academicYearId = null;
            this.homeAddress = null;
            this.lastName = null;
            this.middleName = null;
            this.nationalityId = null;
          
            this.classId = null;
            this.cityOrLocation = null;

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
    computed: {

         
    },
    watch: {

        propertyId(val) {

            this.getPropertyUnits();
        }

    },

    created() {

        this.getProperties();
    },
});