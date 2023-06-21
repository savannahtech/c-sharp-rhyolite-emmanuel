
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        nationalityList: [],
        selectedNationalityId: '',
        reportApiBaseUrl: ''

    },

    methods: {

        getData() {

            if (this.selectedNationalityId) {

                axios.get('/SchReports/GetStudentsByNationality?nationalityId=' + this.selectedNationalityId).then(response => {


                    let nationalityInfo = this.getNationalityDetails(this.selectedNationalityId);

                    response.data.header.forEach((v) => {

                        v.reportTitle = v.reportTitle + ' (' + nationalityInfo.name + ')';

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
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634342936/reportapi/sm/StudentsByNationality.rdl',
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
        
        getNationalities() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.nationalityList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get nationalities.');

            });
        },

        getNationalityDetails(id) {

            return this.nationalityList.find(a => a.id === id);
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

        this.getNationalities();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});