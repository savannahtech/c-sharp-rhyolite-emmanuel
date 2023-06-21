
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        academicYearList: [],
        selectedAcademicYearId: '',
        reportApiBaseUrl: ''
       
    },

    methods: {

        getData() {

            if (this.selectedAcademicYearId) {

                axios.get('/SchReports/GetAlumni?academicYearId=' + this.selectedAcademicYearId).then(response => {

                    let academicYearInfo = this.getAcademicYearDetails(this.selectedAcademicYearId);

                    response.data.header.forEach((v) => {

                        v.reportTitle = v.reportTitle + ' ' + academicYearInfo.name;


                    });
                    this.renderReport(response.data.header, response.data.details);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get data');

                });


            }


        },

        renderReport(header, details) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634842209/reportapi/sm/Alumni.rdl',
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
    
   
    created() {

        this.getAcademicYears();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});