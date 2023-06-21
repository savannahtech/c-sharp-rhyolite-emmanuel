
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        paymentDate: moment().format("YYYY-MM-DD"),
        reportApiBaseUrl: ''
        

    },

    methods: {

        getData() {

            if (this.paymentDate) {

                axios.get('/SchReports/GetDailyPayments?paymentDate=' + this.paymentDate).then(response => {

                    response.data.header.forEach((v) => {

                        v.reportTitle = v.reportTitle + ' ' + this.formatShortDate(this.paymentDate)  ;

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
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634592531/reportapi/sm/DailyPayments.rdl',
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

        this.reportApiBaseUrl = reportApiBaseUrl;
        this.getData();
    },
});