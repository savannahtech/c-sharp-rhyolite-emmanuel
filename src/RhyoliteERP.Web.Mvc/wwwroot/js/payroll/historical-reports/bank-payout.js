﻿
var vue = new Vue({
    el: '#app',
    data: {

        emptyGuid: '00000000-0000-0000-0000-000000000000',
        isLoading: false,
        month: 1,
        year: new Date().getFullYear(),
        payload: {},
        reportType: 'summary-report',
        reportApiBaseUrl: '',
        bankList: [],
        bankId: '',
        reportPath: '',
        reportPathConfig: {

            summaryReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682321393/reportapi/payroll/bank-payment-summary.rdl',
            categoryReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682321393/reportapi/payroll/bank-payment-by-category.rdl',
            departmentReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682321393/reportapi/payroll/bank-payment-by-department.rdl',
            bankReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682321393/reportapi/payroll/bank-payment-summary.rdl',
        },
        
    },

    methods: {

        getReportData() {

            this.isLoading = true;

            let url = `/PayrollReports/HrGetBankPayments?month=${this.month}&year=${this.year}`;

            this.reportPath = this.reportPathConfig.summaryReportPath;

            switch (this.reportType) {

                case 'category':

                    this.reportPath = this.reportPathConfig.categoryReportPath;

                    break;

                case 'department':

                    this.reportPath = this.reportPathConfig.departmentReportPath;

                    break;

                case 'bank':

                    if (!this.bankId) {

                        this.bankId = this.emptyGuid;
                    }

                    this.reportPath = this.reportPathConfig.bankReportPath;
                    url = `/PayrollReports/HrGetBankPaymentsByBank?bankId=${this.bankId}&month=${this.month}&year=${this.year}`;

                    break;

                default:
                    this.reportPath = this.reportPathConfig.summaryReportPath;
                    url = `/PayrollReports/HrGetBankPayments?month=${this.month}&year=${this.year}`;

            }

            
            axios.get(url).then(response => {

                response.data.reportHeader.forEach((v) => {

                    v.reportTitle = `${v.reportTitle}${response.data.reportDetails.currentDate}`;

                });

                this.renderReport(response.data.reportHeader, response.data.reportDetails.data, this.reportPath);

                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get data');
                this.isLoading = false;

            });


        },


        renderReport(header, details, reportPath) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: reportPath,
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

        getBanks() {

            axios.get('/SharedResource/GetBanks').then(response => {


                this.bankList = response.data;

                this.renderTable(response.data, this.bankBranchList);


            }).catch(err => {


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
    
   
    created() {

        this.getReportData();
        this.getBanks();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});