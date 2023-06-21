﻿
var vue = new Vue({
    el: '#app',
    data: {

        isLoading: false,
        payload: {},
        payslipType: 'basic',
        employeeId: '',
        reportApiBaseUrl: '',
        month: 1,
        year: new Date().getFullYear(),
        employeeList: []
       
    },

    methods: {

        getReportData() {

            if (this.employeeId) {

                this.isLoading = true;

                axios.get(`/PayrollReports/HrGetPaySlip?employeeId=${this.employeeId}&payslipType=${this.payslipType}&month=${this.month}&year=${this.year}`).then(response => {

                    response.data.reportHeader.forEach((v) => {

                        v.reportTitle = `${v.reportTitle}${response.data.reportDetails.currentDate}`;

                    });


                    response.data.reportDetails.paymaster.forEach((v) => {

                        v.grossPay = response.data.reportDetails.grossPay;

                    });


                    let reportDetails = response.data.reportDetails;

                    switch (this.payslipType) {

                        case 'basic':

                            this.renderBasicReport(response.data.reportHeader, reportDetails.payslipBaicInfo, reportDetails.employeeDeductions, reportDetails.paymaster);
                            break;

                        case 'type-i':

                            this.renderReportTypeI(response.data.reportHeader, reportDetails.payslipBaicInfo, reportDetails.employeeDeductions, reportDetails.paymaster, reportDetails.employeeAllowances);
                            break;

                        case 'type-ii':

                            this.renderReportTypeII(response.data.reportHeader, reportDetails.payslipBaicInfo, reportDetails.employeeDeductions, reportDetails.paymaster, reportDetails.employeeAllowances, reportDetails.employeeReliefs );
                            break;

                        case 'type-iii':

                            this.renderReportTypeIII(response.data.reportHeader, reportDetails.payslipBaicInfo, reportDetails.employeeDeductions, reportDetails.paymaster, reportDetails.employeeAllowances, reportDetails.employeeReliefs, reportDetails.employeeBenefitsInKind, reportDetails.employeeLoans, reportDetails.employeeSalaryAdvance, reportDetails.employeeOvertime);
                            break;
                        default:
                    }

                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get data.');

                    this.isLoading = false;

                });

            }
            else {
                this.toastHandler('warning', 'Select an employee');

            }

        },

        renderBasicReport(header, payslipBaicInfo, employeeDeductions, paymaster) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1684123114/reportapi/payroll/payslip-basic.rdl',
                dataSources: [{
                    value: header,
                    name: "DataSet1"
                },
                {
                    value: payslipBaicInfo,
                    name: "DataSet2"

                    },
                {
                    value: paymaster,
                    name: "DataSet3"
                },
                {
                    value: employeeDeductions,
                    name: "DataSet4"
                }]

            });

        },

        renderReportTypeI(header, payslipBaicInfo, employeeDeductions, paymaster, employeeAllowances) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1684123114/reportapi/payroll/payslip-type-i.rdl',
                dataSources: [{
                    value: header,
                    name: "DataSet1"
                },
                {
                    value: payslipBaicInfo,
                    name: "DataSet2"
                },
                {
                    value: paymaster,
                    name: "DataSet3"
                },
                {
                    value: employeeDeductions,
                    name: "DataSet4"
                },
                {
                    value: employeeAllowances,
                    name: "DataSet5"
                }]

            });

        },

        renderReportTypeII(header, payslipBaicInfo, employeeDeductions, paymaster, employeeAllowances, employeeReliefs) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1684125026/reportapi/payroll/payslip-type-ii.rdl',
                dataSources: [{
                    value: header,
                    name: "DataSet1"
                },
                {
                    value: payslipBaicInfo,
                    name: "DataSet2"
                },
                {
                    value: paymaster,
                    name: "DataSet3"
                },
                {
                    value: employeeDeductions,
                    name: "DataSet4"
                },
                {
                    value: employeeAllowances,
                    name: "DataSet5"
                },
                {
                    value: employeeReliefs,
                    name: "DataSet6"
                }]

            });

        },

        renderReportTypeIII(header, payslipBaicInfo, employeeDeductions, paymaster, employeeAllowances, employeeReliefs, employeeBenefitsInKind, employeeLoans, employeeSalaryAdvance, employeeOvertime ) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1684144759/reportapi/payroll/payslip-type-iii.rdl',
                dataSources: [{
                    value: header,
                    name: "DataSet1"
                },
                {
                    value: payslipBaicInfo,
                    name: "DataSet2"
                },
                {
                    value: paymaster,
                    name: "DataSet3"
                },
                {
                    value: employeeDeductions,
                    name: "DataSet4"
                },
                {
                    value: employeeAllowances,
                    name: "DataSet5"
                },
                {
                    value: employeeReliefs,
                    name: "DataSet6"
                },
                {
                    value: employeeBenefitsInKind,
                    name: "DataSet7"
                },
                {
                    value: employeeLoans,
                    name: "DataSet8"
                },
                {
                    value: employeeSalaryAdvance,
                    name: "DataSet9"
                },
                {
                    value: employeeOvertime,
                    name: "DataSet10"
                }
                 
                ]

            });

        },

        getEmployees() {

            axios.get('/PayrollDataEntry/GetEmployees').then(response => {

                response.data.forEach(v => {

                    v.employeeName = v.otherName == '' || null ? `${v.firstName} ${v.lastName} - ${v.employeeIdentifier}` : `${v.firstName} ${v.lastName} ${v.otherName} - ${v.employeeIdentifier}`

                });

                this.employeeList = response.data;

                $("#employeeId").ejDropDownList({
                    dataSource: response.data,
                    watermarkText: "Select Employee",
                    width: "100%",
                    fields: { id: "id", text: "employeeName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.employeeId = args.value;
                    }

                });


            }).catch(err => {

                //console.log(err);
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

        this.getEmployees();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});