
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        schClasses: [],
        academicYearList: [],
        termList: [],
        subjectList: [],
        resultTypeList: [],
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        selectedSubjectId: '',
        selectedResultTypeId: '',
        reportApiBaseUrl: ''


    },
   
    methods: {

        getAcademicYears() {

            axios.get('/SchSetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },
        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },
        getSubjects() {

            axios.get('/SchSetups/GetSubjects').then(response => {

                this.subjectList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get subjects.');

            });
        },

        getResultTypes() {

            axios.get('/SchSetups/GetResultTypesByClass?id=' + this.selectedClassId).then(response => {

                this.resultTypeList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },
      
        requestBulkTerminalReports() {

            this.isLoading = true;

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedSubjectId) {

                let academicYearInfo = this.getAcademicYearDetails(this.selectedAcademicYearId);
                let termInfo = this.getTermDetails(this.selectedTermId);
                let classInfo = this.getClassDetails(this.selectedClassId);
                let subjectInfo = this.getSubjectDetails(this.selectedSubjectId);
                let resultTypeInfo = this.getResultTypeDetails(this.selectedResultTypeId);

                

                this.payload.reportOptions = JSON.stringify({

                    'AcademicYearId': this.selectedAcademicYearId,
                    'SubjectId': this.selectedSubjectId,
                    'Subject': subjectInfo.name,
                    'ResultTypeId': this.selectedResultTypeId,
                    'Result Type': resultTypeInfo.name,
                    'TermId': this.selectedTermId,
                    'ClassId': this.selectedClassId,
                    'Academic Year': academicYearInfo.name,
                    'Term': termInfo.name,
                    'Class': classInfo.className
                });
                this.payload.name = 'Terminal Report';
                this.payload.reportKey = 'terminal-report';


                axios.post('/SchReports/RequestReportDownload', this.payload).then(response => {

                    this.toastHandler('', 'Your download request has been queued for processing.');
                    this.isLoading = false;

                    setTimeout(() => { window.location.href = '/Manage/Downloads'; }, 4000);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to process request.');
                    this.isLoading = false;

                });


            }


        },
     
        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
        },
        getTermDetails(id) {

            return this.termList.find(a => a.id === id);
        },
        getSubjectDetails(id) {

            return this.subjectList.find(a => a.id === id);
        },
        getResultTypeDetails(id) {

            return this.resultTypeList.find(a => a.id === id);
        },
        getClassDetails(id) {
            return this.classList.find(a => a.id === id);
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

        this.getSchClasses();
        this.getAcademicYears();
        this.getSubjects();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});