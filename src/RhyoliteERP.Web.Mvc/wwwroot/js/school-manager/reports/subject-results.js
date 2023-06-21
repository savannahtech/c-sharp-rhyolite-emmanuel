
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

        getData() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedSubjectId && this.selectedResultTypeId) {

                axios.get('/SchReports/GetTerminalSubjectResults?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&subjectId=' + this.selectedSubjectId + '&resultTypeId=' + this.selectedResultTypeId).then(response => {

                    let subjectInfo = this.getSubjectDetails(this.selectedSubjectId);

                    let classInfo = this.getClassDetails(this.selectedClassId);

                    response.data.header.forEach((v) => {

                        v.reportTitle = v.reportTitle + ' ' + classInfo.className + ' (' + subjectInfo.name + ')';

                    });

                    this.renderReport(response.data.header, response.data.details);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get data.');

                });


            }


        },
        renderReport(header, details) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634840146/reportapi/sm/TerminalResults.rdl',
                dataSources: [{
                    value: header,
                    name: "DataSet1"
                },
                {
                    value: details,
                    name: "DataSet2"
                }]

            });

        },
     
        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
        },

        getSubjectDetails(id) {

            return this.subjectList.find(a => a.id === id);
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