Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

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
        academicYearId: '',
        termId: '',
        classId: '',
        subjectId: '',
        resultTypeId: '',
        totalMarks: 0,
        uploadFile: null,
        terminalReportType: 'standard'

    },
   
    methods: {

        downloadFile() {

            if (this.classId) {

                window.location.href = `/SchImportData/DownloadResultSample?classId=${this.classId}`;

            }
            else {
                window.location.href = '/SchImportData/DownloadResultSample';

            }
        },
         
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

            axios.get('/SchSetups/GetResultTypesByClass?id='+this.classId).then(response => {

                this.resultTypeList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get result types.');

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
    computed: {

        classList() {

            this.schClasses.forEach((v) => {

                if (v.streamId != '00000000-0000-0000-0000-000000000000') {
                    v.className = v.className + '-' + v.streamName;
                }

            });

            return this.schClasses;
        },
        enableUpload() {

            return this.academicYearId != '' && this.termId != '' && this.classId != '' && this.subjectId != '' && this.resultTypeId != '' && this.totalMarks > 0;
        }
    },
    watch: {

        
        academicYearId(val) {

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
    },
});