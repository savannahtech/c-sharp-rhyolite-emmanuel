
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        religionList: [],
        selectedReligionId: '',
        reportApiBaseUrl: ''

    },

    methods: {

        getData() {

            if (this.selectedReligionId) {

                axios.get('/SchReports/GetStudentsByReligion?religionId=' + this.selectedReligionId).then(response => {

                    let religionInfo = this.getReligionDetails(this.selectedReligionId);

                    response.data.header.forEach((v) => {

                        v.reportTitle = v.reportTitle + ' (' + religionInfo.name + ')';

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
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634342936/reportapi/sm/StudentsByReligion.rdl',
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
        getReligionDetails(id) {

            return this.religionList.find(a => a.id === id);
        },
        
        getReligions() {

            axios.get('/SharedResource/GetReligions').then(response => {

                this.religionList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get religions.');

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
    
   
    created() {

        this.getReligions();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});