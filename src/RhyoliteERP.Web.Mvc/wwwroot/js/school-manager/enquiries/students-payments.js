﻿
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
        schClasses: [],

    },

    methods: {

        getData() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId) {


                axios.get('/SchEnquiries/GetStudentPayments?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId ).then(response => {


                    if (response.data) {

                        response.data.forEach((v) => {

                            v.paymentDate = this.formatShortDate(v.paymentDate);
                            v.acaTerm = v.academicYearName + '-' + v.termName;
                        });

                        this.renderTable(response.data);

                    }



                }).catch(err => {

                    this.toastHandler('error', 'Unable to get payments');

                });


            }

           
        },

        renderTable(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                pageSettings: { pageSize: 10 },
                isResponsive: true,
                allowGrouping: true,
                allowSorting: true,
                sortSettings: { sortedColumns: [{ field: "studentName", direction: "ascending" }] },
                columns: [

                    { field: "id", visible: false, width: 10, isPrimaryKey: true, },
                    { field: "studentIdentifier", headerText: "Student ID", width: 50, },
                    { field: "studentName", headerText: "Student  Name", width: 100, },
                    { field: "amountPaid", format: "{0:n2}", textAlign: ej.TextAlign.Right, headerText: "Amount Paid", width: 50, },
                    { field: "acaTerm", headerText: "Academic Year/Term", width: 65, },
                    { field: "paymentDate", format: "{0:dd-MMM-yyyy}", headerText: "Payment Date", width: 55, },
                    { field: "modeOfPayment", headerText: "Mode Of Payment", width: 70, },
                    { field: "currencyName", headerText: "Currency", width: 55, },
                    { field: "receiptNo", headerText: "Receipt No#", width: 55, },
                ]
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
        getSchClasses() {

            axios.get('/schsetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getBillTypes() {

            axios.get('/schsetups/GetBillTypes').then(response => {

                this.billTypeList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get bill types.');

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
        }
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

        this.getAcademicYears();
        this.getSchClasses();
        this.getBillTypes();
    },
});