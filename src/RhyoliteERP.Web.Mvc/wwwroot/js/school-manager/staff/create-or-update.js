Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        staffList: [],
        staffStatusList: [],
        religionList: [],
        nationalityList: [],
        staffIdentifier: '',
        selectedStaffId: '',
        id: null,
        tenantId: null,
        firstName: '',
        dateOfBirth: '',
        dateEmployed: '',
        religionId: '',
        homeAddress: '',
        staffStatusId: '',
        lastName: '',
        otherName: '',
        nationalityId: '',
        gender: 'Male',
        cityOrLocation: '',
        emergencyContactPhone: '',
        secondaryPhone: '',
        maritalStatus: 'Single',
        homeAddress: '',
        emailAddress: '',
        emergencyContactPerson: '',
        primaryPhone: '',
        ssn: '',
        staffInfo: {},
        staffListControllInitialized: false

    },
    validations() {

        return {

            staffIdentifier: {
                required
            },
            firstName: {
                required
            },
            lastName: {
                required, 
            },
            staffStatusId: {
                required,
            },
            primaryPhone: {
                required,
            },
            cityOrLocation: {
                required,
            },
            

        }


    },
    methods: {

        saveStaff() {

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


            this.payload = {};
            this.isLoading = true;

            if (this.dateOfBirth == '') {

                this.dateOfBirth = new Date();
            }

            if (this.dateEmployed == '') {

                this.dateEmployed = new Date();
            }
 
            if (this.nationalityId == '') {

                this.nationalityId = '00000000-0000-0000-0000-000000000000';
            }

            if (this.staffStatusId == '') {

                this.staffStatusId = '00000000-0000-0000-0000-000000000000';
            }

            this.payload.staffIdentifier = this.staffIdentifier;
            this.payload.firstName = this.firstName;
            this.payload.dateOfBirth = this.dateOfBirth;
            this.payload.dateEmployed = this.dateEmployed;
            this.payload.ssn = this.ssn;
            this.payload.primaryPhone = this.primaryPhone;
            this.payload.emergencyContactPerson = this.emergencyContactPerson;
            this.payload.emailAddress = this.emailAddress;
            this.payload.homeAddress = this.homeAddress;
            this.payload.lastName = this.lastName;
            this.payload.nationalityId = this.nationalityId;
            this.payload.gender = this.gender;
            this.payload.otherName = this.otherName;
            this.payload.cityOrLocation = this.cityOrLocation;
            this.payload.staffStatusId = this.staffStatusId;
            this.payload.maritalStatus = this.maritalStatus;
            this.payload.secondaryPhone = this.secondaryPhone;
            this.payload.emergencyContactPhone = this.emergencyContactPhone;
            this.payload.isTeachingStaff = true;
             

            if (this.id != null) {

                this.payload.id = this.id;
                this.payload.tenantId = this.tenantId;

                axios.post('/Staff/UpdateStaff', this.payload).then((response) => {

                    this.isLoading = false;
                    this.toastHandler('', 'Staff info updated.');
                    this.id = null;
                    this.getStaff();
                    this.resetForm();


                }).catch((error) => {

                    this.isLoading = false;
                    this.toastHandler('error', 'Unable to update staff.');

                });

            }
            else {

                axios.post('/Staff/CreateStaff', this.payload).then((response) => {

                    this.isLoading = false;
                    this.toastHandler('', 'Staff info saved.');
                    this.id = null;
                    this.getStaff();
                    //this.resetForm();

                }).catch((error) => {

                    this.isLoading = false;
                    this.toastHandler('error', 'Unable to create staff.');

                });

            }

        },
       
        getStaff() {

            axios.get('/Staff/GetStaff').then(response => {

                this.staffList = response.data;

                response.data.forEach(v => {

                    v.staffName = v.otherName == '' || null ? `${v.lastName} ${v.firstName} - ${v.staffIdentifier}` : `${v.lastName} ${v.firstName} ${v.middleName} - ${v.staffIdentifier}`

                });


                $("#staffIdQuery").ejDropDownList({

                    dataSource: response.data.sort((a, b) => a.staffName.localeCompare(b.staffName)),
                    watermarkText: "Select Staff",
                    width: "100%",
                    fields: { id: "id", text: "staffName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.selectedStaffId = args.value;
                    }

                });

                this.staffListControllInitialized = true;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff.');

            });

        },

        generateStaffIdentifier() {

            this.staffIdentifier = Math.floor(Math.random() * 99999) + 100;

        },
        getNationalities() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.nationalityList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get nationalities.');

            });
        },
        
        getStaffStatus() {

            axios.get('/SchSetups/GetStatus').then(response => {

                this.staffStatusList = response.data;
                if (this.staffStatusList.length) {

                    this.staffStatusId = this.staffStatusList[0].id;

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff status.');

            });
        },
         
      
        getStaffDetail() {

            axios.get('/Staff/GetStaffDetails?id=' + this.selectedStaffId).then(response => {
 
                
                this.staffIdentifier = response.data.staffIdentifier;
                this.firstName = response.data.firstName;
                this.dateOfBirth = this.formatDate(response.data.dateOfBirth);
                this.dateEmployed = this.formatDate(response.data.dateEmployed);
                this.ssn = response.data.ssn;
                this.primaryPhone = response.data.primaryPhone;
                this.emergencyContactPerson = response.data.emergencyContactPerson;
                this.emailAddress = response.data.emailAddress;
                this.homeAddress = response.data.homeAddress;
                this.lastName = response.data.lastName;
                this.nationalityId = response.data.nationalityId;
                this.gender = response.data.gender;
                this.otherName = response.data.otherName;
                this.staffStatusId = response.data.staffStatusId;
                this.maritalStatus = response.data.maritalStatus;
                this.secondaryPhone = response.data.secondaryPhone;
                this.emergencyContactPhone = response.data.emergencyContactPhone;
                this.cityOrLocation = response.data.cityOrLocation;
                this.id = response.data.id;
                this.tenantId = response.data.tenantId;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff detail');

            });

        },
      
        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

        },
        resetForm() {

            this.staffIdentifier = null;

            this.firstName = null;

            this.dateOfBirth = moment().format("YYYY-MM-DD");
            this.dateEmployed = moment().format("YYYY-MM-DD");
            this.ssn = null
            this.primaryPhone = null;

            this.emailAddress = null;
            this.homeAddress = null;
            this.lastName = null;

            this.nationalityId = null;
            this.otherName = null;

            this.staffStatusId = null;
            this.maritalStatus = null;
            this.secondaryPhone = null;
            this.emergencyContactPhone = null;
            this.emergencyContactPerson = null;
            this.cityOrLocation = null;
            this.id = null;

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

            this.scrollTop();


        },
        scrollTop() {

            window.scroll({
                top: 0,
                left: 0,
                behavior: 'smooth'
            });

        }
        
    },
    computed: {

        
    },
    watch: {

       
        selectedStaffId(val) {

            this.getStaffDetail();
             
        }

    },

    created() {

        this.getNationalities();
        this.getStaffStatus();
        this.getStaff();
        
    },
});