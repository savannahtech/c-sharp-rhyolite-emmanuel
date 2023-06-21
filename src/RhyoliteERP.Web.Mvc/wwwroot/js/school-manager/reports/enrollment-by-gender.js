
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        enrollmentData: [],
        selectedReligionId: '',
        reportApiBaseUrl: ''

    },

    methods: {

        getData() {

            axios.get('/SchReports/GetEnrollmentByGender').then(response => {


                response.data.details.forEach((v) => {

                    if (v.gender == 'Male') {

                        this.enrollmentData.push({

                            className: v.className,
                            maleNo: v.total,
                            femaleNo: 0,
                            totalNo: v.total,
                        });
                    }
                    else if (v.gender == 'Female') {

                        this.enrollmentData.push({
                            className: v.className,
                            femaleNo: v.total,
                            maleNo: 0,
                            totalNo: v.total

                        });
                    }

                });

                let results = this.enrollmentData.reduce((res, obj) => {

                    if (!(obj.className in res))
                        res.__array.push(res[obj.className] = obj);
                    else {
                        res[obj.className].totalNo += obj.totalNo;
                    }
                    return res;
                }, { __array: [] }).__array
                    .sort((a, b) => { return b.totalNo - a.totalNo; });

                this.enrollmentData.forEach((a) => {

                    results.forEach((b) => {

                        if (a.className == b.className) {
                            a.totalNo = b.totalNo;
                        }

                    });

                });

                let genderEnrollmentData = this.enrollmentData.reduce((res, obj) => {

                    if (!(obj.className in res))
                        res.__array.push(res[obj.className] = obj);
                    else {
                        res[obj.className].maleNo += obj.maleNo;
                        res[obj.className].femaleNo += obj.femaleNo;
                    }
                    return res;
                }, { __array: [] }).__array
                    .sort((a, b) => { return b.totalNo - a.totalNo; });


                this.renderReport(response.data.header, genderEnrollmentData);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get data.');

            });

           
        },

        renderReport(header, details) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634342936/reportapi/sm/EnrollmentByGender.rdl',
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

        this.getData();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});