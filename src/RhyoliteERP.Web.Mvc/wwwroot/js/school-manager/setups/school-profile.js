
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        selectedAcademicYearId: '',
        selectedTermId: '',
        academicYearList: [],
        termList: [],
        schoolName: '',
        secondaryEmailAddress: '',
        regionOrState: '',
        city: '',
        primaryPhoneNo: '',
        schoolHead: '',
        assistantSchoolHead: '',
        website: '',
        postalAddress: '',
        district: '',
        streetAddress: '',
        accountant: '',
        autoEmailReceiptNotification: false,
        autoSMSReceiptNotification: false,
        mailHostName: '',
        primaryEmailAddress: '',
        emailPassword: '',
        isSSLEnabled: 'true',
        schoolLogoUrl: '',
        portNumber: 0,
        emptyGuid: '00000000-0000-0000-0000-000000000000',

    },

    methods: {

        getSchoolProfile() {

            axios.get('/schsetups/GetSchoolProfile').then(response => {

                let schData = response.data;
                if (schData) {

                    this.schoolName = schData.schoolName;
                    this.secondaryEmailAddress = schData.secondaryEmailAddress;
                    this.regionOrState = schData.regionOrState;
                    this.city = schData.city;
                    this.primaryPhoneNo = schData.primaryPhoneNo;
                    this.schoolHead = schData.schoolHead;
                    this.assistantSchoolHead = schData.assistantSchoolHead;
                    this.website = schData.website;
                    this.postalAddress = schData.postalAddress;
                    this.portNumber = schData.portNumber;
                    this.district = schData.district;
                    this.streetAddress = schData.streetAddress;
                    this.accountant = schData.accountant;
                    this.autoEmailReceiptNotification = schData.autoEmailReceiptNotification;
                    this.autoSMSReceiptNotification = schData.autoSMSReceiptNotification;
                    this.selectedAcademicYearId = schData.currentAcademicYearId;
                    this.selectedTermId = schData.currentTermId;
                    this.mailHostName = schData.mailHostName;
                    this.primaryEmailAddress = schData.primaryEmailAddress;
                    this.emailPassword = schData.emailPassword;
                    this.isSSLEnabled = schData.isSSLEnabled;

                }
                


            }).catch(err => {

                this.toastHandler('error', 'Unable to get school profile.');

            });
        },
 
        saveProfile() {

            this.isLoading = true;

            let payload = {};

            payload.currentAcademicYearId = this.selectedAcademicYearId;
            payload.currentTermId = this.selectedTermId;

            if (this.selectedAcademicYearId == '') {
                payload.currentAcademicYearId = this.emptyGuid;
            }

            if (this.selectedTermId == '') {
                payload.currentTermId = this.emptyGuid;
            }

            
            payload.schoolName = this.schoolName;
            payload.secondaryEmailAddress = this.secondaryEmailAddress;
            payload.regionOrState = this.regionOrState;
            payload.city = this.city;
            payload.primaryPhoneNo = this.primaryPhoneNo;
            payload.schoolHead = this.schoolHead;
            payload.assistantSchoolHead = this.assistantSchoolHead;
            payload.website = this.website;
            payload.postalAddress = this.postalAddress;
            payload.district = this.district;
            payload.streetAddress = this.streetAddress;
            payload.accountant = this.accountant;
            payload.autoEmailReceiptNotification = this.autoEmailReceiptNotification;
            payload.autoSMSReceiptNotification = this.autoSMSReceiptNotification;
            payload.mailHostName = this.mailHostName;
            payload.primaryEmailAddress = this.primaryEmailAddress;
            payload.portNumber = this.portNumber;
            payload.emailPassword = this.emailPassword;
            payload.isSSLEnabled = this.isSSLEnabled;
            payload.schoolLogoUrl = this.schoolLogoUrl;
            
            
            axios.post('/schsetups/SaveProfile', payload).then(response => {

                this.isLoading = false;

                this.toastHandler('', 'Profile saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save profile.');
                this.isLoading = false;

            });

        },
        getAcademicYears() {

            axios.get('/schsetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;


            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },

        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
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

        selectedAcademicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }

        },

    },
    created() {

        this.getSchoolProfile();

        this.getAcademicYears();
    },
});