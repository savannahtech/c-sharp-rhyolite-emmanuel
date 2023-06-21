
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        academicYearList: [],
        termList: [],
        schClasses: [],
        studentList: [],
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        schClasses: [],
        reportApiBaseUrl: ''


    },

    methods: {


        requestBulkBills() {

            this.isLoading = true;

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId) {

                let academicYearInfo = this.getAcademicYearDetails(this.selectedAcademicYearId);
                let termInfo = this.getTermDetails(this.selectedTermId);
                let classInfo = this.getClassDetails(this.selectedClassId);

                this.payload.reportOptions = JSON.stringify({ 'AcademicYearId': this.selectedAcademicYearId, 'TermId': this.selectedTermId, 'ClassId': this.selectedClassId, 'Academic Year': academicYearInfo.name, 'Term': termInfo.name, 'Class': classInfo.className });
                this.payload.name = 'Student Bills';
                this.payload.reportKey = 'student-bill';

                axios.post('/SchReports/RequestReportDownload', this.payload).then(response => {

                    this.toastHandler('', 'Your download request has been queued for processing.');
                    this.isLoading = false;

                    setTimeout(() => { window.location.href = '/Manage/Downloads'; }, 3000);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to process request.');
                    this.isLoading = false;

                });


            }

           

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

        getClassDetails(id) {
            return this.classList.find(a => a.id === id);
        },
        getTermDetails(id) {

            return this.termList.find(a => a.id === id);
        },
        getSchClasses() {

            axios.get('/schsetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

        },

        formatShortDate(dateInput) {

            return moment(dateInput).format("DD-MMM-YYYY");

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

        classList() {

            this.schClasses.forEach((v) => {

                if (v.streamId != '00000000-0000-0000-0000-000000000000') {
                    v.className = v.className + '-' + v.streamName;
                }

            });

            return this.schClasses;
        }
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

        this.getAcademicYears();
        this.getSchClasses();

    },
});