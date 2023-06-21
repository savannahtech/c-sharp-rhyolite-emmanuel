﻿
var vue = new Vue({
    el: '#app',
    data: {

        emptyGuid: '00000000-0000-0000-0000-000000000000',
        isLoading: false,
        payload: {},
        reportType: 'summary-report',
        reportApiBaseUrl: '',
        allowanceTypeList: [],
        allowanceTypeId: '',
        categoryList: [],
        departmentList: [],
        departmentNameList: [],
        departmentHierachyList: [],
        reportPath: '',
        reportPathConfig: {

            summaryReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682250731/reportapi/payroll/allowance-summary.rdl',
            categoryReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682250731/reportapi/payroll/allowance-by-category.rdl',
            departmentReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682250731/reportapi/payroll/allowance-by-department.rdl',
            allowanceTypeReportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1682251317/reportapi/payroll/allowance-by-type.rdl',

        },
        
    },

    methods: {

        getReportData() {

            this.isLoading = true;
            let url = '/PayrollReports/GetAllowances';

            this.reportPath = this.reportPathConfig.summaryReportPath;

            switch (this.reportType) {

                case 'allowance-type':

                    this.reportPath = this.reportPathConfig.allowanceTypeReportPath;

                    if (!this.allowanceTypeId)
                    {
                        this.allowanceTypeId = this.emptyGuid;
                    }

                    url = `/PayrollReports/GetAllowancesByType?allowanceTypeId=${this.allowanceTypeId}`;

                    break;

                case 'category':

                    this.reportPath = this.reportPathConfig.categoryReportPath;

                    break;

                case 'department':

                    this.reportPath = this.reportPathConfig.departmentReportPath;

                    break;



                default:
                    this.reportPath = this.reportPathConfig.summaryReportPath;
                    url = '/PayrollReports/GetAllowances';

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

        getCategories() {

            axios.get('/PayrollSetups/GetEmployeeCategories').then(response => {

                this.categoryList = response.data;

            }).catch(err => {

                console.log(err);

                this.toastHandler('error', 'Unable to get employee categories.');

            });
        },

        getDepartments() {

            axios.get('/SharedResource/GetDepartments').then(response => {

                this.parseDepartmentHierachy(response.data);

            }).catch(err => {

                console.log(err);

                this.toastHandler('error', 'Unable to get departments.');

            });
        },

        getAllowanceTypes() {

            axios.get('/PayrollSetups/GetAllowanceTypes').then(response => {

                this.allowanceTypeList = response.data.allowanceTypes;

            }).catch(err => {

                console.log(err);
            });
        },

        parseDepartmentHierachy(dataArr) {

            this.departmentList = dataArr;

            var departmentHierachyList = [];

            for (var i = 0; i < this.departmentList.length; i++) {

                this.getDepartmentHierachyName(this.departmentList[i].id);

                var name = [...new Set(this.departmentNameList)].join().replace(/,/g, '/');

                departmentHierachyList.push({
                    name: name,
                    parentId: this.departmentList[i].parentId,
                    id: this.departmentList[i].id,
                    rawName: this.departmentList[i].name,
                    tenantId: this.departmentList[i].tenantId,
                });

                name = [];

                this.departmentNameList = [];

            }

            this.departmentHierachyList = departmentHierachyList;

        },

        getDepartmentHierachyName(id) {

            var parentId;
            var returnName;
            this.departmentList.forEach(item => {

                if (item.id == id) {
                    parentId = item.parentId;
                    returnName = item.name;
                }

                this.departmentList.forEach(cc => {
                    if (cc.id == parentId) {
                        return (`${this.getDepartmentHierachyName(parentId)} / ${returnName}`);
                    }
                })

            });

            this.departmentNameList.push(returnName);

        },


        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");
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
        this.getAllowanceTypes();
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});