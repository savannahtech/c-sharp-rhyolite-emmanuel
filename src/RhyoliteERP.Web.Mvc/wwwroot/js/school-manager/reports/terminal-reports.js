
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        academicYearList: [],
        termList: [],
        schClasses: [],
        billTypeList: [],
        studentList: [],
        selectedBillTypeId: '',
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        selectedStudentId: '',
        reportApiBaseUrl: ''
        

    },
    methods: {


        getTerminalReport() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedStudentId) {


                axios.get('/SchReports/GetTerminalReport?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&studentId=' + this.selectedStudentId).then(response => {

                    let classInfo = this.getClassDetails(this.selectedClassId);

                    let detailsArray = [];
                    let summaryArray = [];

                    response.data.header.forEach((v) => {

                        v.reportTitle = v.reportTitle + ' ' + classInfo.className;
                    });


                    detailsArray.push(response.data.details);

                    let subjectResults = response.data.details.subjectResults;
                     summaryArray.push(response.data.summary);

                    this.renderReport(response.data.header, detailsArray, subjectResults, summaryArray);


                }).catch(err => {

                    this.toastHandler('error', 'Unable to get report data.');

                });

            }

        },

        renderReport(header, basicInfo,subjectResults,summaryInfo) {


            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1668347817/reportapi/sm/TerminalReport.rdl',
                dataSources: [{
                    value: header,
                    name: "DataSet1"
                },
                {
                    value: basicInfo,
                    name: "DataSet2"
                },
                {
                    value: subjectResults,
                    name: "DataSet3"
                },
                {
                    value: summaryInfo,
                    name: "DataSet4"
                }]

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

        getTermDetails(id) {

            return this.termList.find(a => a.id === id);
        },

        getClassDetails(id) {

            return this.classList.find(a => a.id === id);
        },

        getCurrencyDetails(id) {

            return this.currencyList.find(a => a.id === id);
        },

        getSchClasses() {

            axios.get('/schsetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getStudents() {

            axios.get('/Students/GetStudentByClass?id=' + this.selectedClassId).then(response => {

                this.studentList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get students.');

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
        },

        displayChequeInput() {

            return this.modeOfPayment == 'Cheque';
        }
    },
    watch: {

        selectedAcademicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }

        },

        selectedClassId(val) {

            this.getStudents();
        },
    },
    created() {

        this.getAcademicYears();
        this.getSchClasses();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});