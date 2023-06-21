
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
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
        studentPayments: [],

    },

    methods: {

        getPayments() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId) {


                axios.get('/BillsAndPayments/GetPostedPayments?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&billTypeId=' + this.selectedBillTypeId).then(response => {

                    response.data.forEach((v) => {

                        v.acaTerm = v.academicYearName + '-' + v.termName;
                        v.paymentDate = this.formatShortDate(v.paymentDate);

                    });

                    this.renderTable(response.data);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get payments');

                });


            }

           
        },

        renderTable(responseData1) {

            $("#dataTable").ejGrid({
                dataSource: responseData1,
                allowPaging: true,
                pageSettings: { pageSize: 10 },
                isResponsive: true,
                allowGrouping: true,
                allowSorting: true,
                enableDropAreaAnimation: true,
                sortSettings: { sortedColumns: [{ field: "studentName", direction: "ascending" }] },
               
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Search, ej.Grid.ToolBarItems.PrintGrid] },
                editSettings: { allowDeleting: true, showDeleteConfirmDialog: true },
                columns: [

                    { type: "checkbox", width: 15 },
                    { field: "id", visible: false, width: 10, isPrimaryKey: true, },
                    { field: "billHeaderId", visible: false, width: 10, },
                    { field: "studentIdentifier", headerText: "Student ID", width: 50, },
                    { field: "studentName", headerText: "Student  Name", width: 105, },
                    { field: "amountPaid", format: "{0:n2}", textAlign: ej.TextAlign.Right, headerText: "Amount Paid", width: 50, },
                    { field: "acaTerm", headerText: "Academic Year/Term", width: 70, },
                    { field: "paymentDate", format: "{0:dd-MMM-yyyy}", headerText: "Payment Date", width: 50, },
                    { field: "modeOfPayment", headerText: "Mode Of Payment", width: 65, },
                    { field: "currencyName", headerText: "Currency", width: 55, },
                    { field: "receiptNo", headerText: "Receipt No#", width: 50, },
                ]
            });

        },

        cancelPayments() {

            this.isLoading = true;
            this.payload = {};

            this.getPaymentsData();

            if (this.studentPayments.length) {

                axios.post('/BillsAndPayments/CancelStudentPayments', this.studentPayments).then(response => {

                    this.isLoading = false;
                    this.toastHandler('', 'Payment(s) cancelled successfully.');
                    this.getPayments();

                }).catch(err => {

                    this.toastHandler('error', 'Unable to cancel payments.');
                    this.isLoading = false;

                });
            }
            else {

                this.isLoading = false;

                this.toastHandler('warning', 'No payments selected.');

            }

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

        getPaymentsData() {

            var obj = $("#dataTable").ejGrid("instance"), rec = []; this.studentPayments = [];
            var check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
            if (check.length) {
                for (var pageInx = 0; pageInx < check.length; pageInx++) {
                    if (!ej.isNullOrUndefined(check[pageInx]))
                        rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                }
            }

            this.studentPayments = rec;

        },

        getRecords(pageInx, inx, proxy, rec) {

            if (inx.length) {
                for (var i = 0; i < inx.length; i++) {
                    var pageSize = proxy.model.pageSettings.pageSize;  //gets the page size of grid
                    var data = proxy.model.dataSource[pageInx * pageSize + inx[i]];
                    rec.push(data);     //pushing all the selected Records
                }
            }
            return rec;

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