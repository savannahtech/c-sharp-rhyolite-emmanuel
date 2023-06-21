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
        academicYearId: '',
        termId: '',
        classId: '',
        resultTypeId: '',
        uploadFile: null

    },
   
    methods: {

        downloadFile() {

            if (this.classId) {

                window.location.href = `/SchImportData/DownloadStudentsUploadSample?classId=${this.classId}`;

            }
            else {
                window.location.href = '/SchImportData/DownloadStudentsUploadSample';

            }
        },
         
        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

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

            return this.classId != '';
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
    },
});