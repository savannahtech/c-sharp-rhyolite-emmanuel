Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

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
        selectedCurrencyId: '',
        selectedStudentId: '',
        schClasses: [],
        paymentsData: [],
        billdata: [],
        totalbillAmount: 0,
        billResidue: [],
        currencyList: [],
        receiptNo: Math.floor((Math.random() * 999999) + 10000),
        modeOfPayment: 'Cash',
        amountPaid: null,
        paymentDate: moment().format("YYYY-MM-DD"),
        isReceiptReady: false,
        chequeNo: ''

    },
    validations() {

        return {

            amountPaid: {
                required
            },

        }


    },
    methods: {


        printPayReceipt() {


        },

         
        renderPaymentTable() {

            $("#dataTable").ejGrid({
                dataSource: this.billResidue,
                allowPaging: true,
                pageSettings: { pageSize: 10 },
                isResponsive: true,
                allowGrouping: true,
                allowSorting: true,
                enableDropAreaAnimation: true,
                groupSettings: { groupedColumns: ["billBalance"] },
                sortSettings: { sortedColumns: [{ field: "AcaTerm", direction: "ascending" }] },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.PrintGrid] },
                summaryRows: [

                    { title: "Balance Due", summaryColumns: [{ summaryType: ej.Grid.SummaryType.Sum, displayColumn: "billBalance", dataMember: "billBalance", format: "{0:n2}" }] },

                ],
                columns: [
                    { field: "id", headerText: 'Id', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "classId", headerText: 'ClassId', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "studentId", headerText: 'StudentId', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "studentID", headerText: 'StudentID', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "acaTerm", headerText: 'Academic Year/Term', textAlign: ej.TextAlign.Left, width: 60 },
                    { field: "className", headerText: 'Class', textAlign: ej.TextAlign.Left, width: 55 },
                    { field: "billNo", headerText: 'Bill No.', textAlign: ej.TextAlign.Left, width: 35 },
                    { field: "billDate", headerText: 'Bill Date', textAlign: ej.TextAlign.Left, width: 35 },
                    { field: "billAmount", headerText: 'Bill Amount', format: "{0:n2}", textAlign: ej.TextAlign.Right, width: 40 },
                    { field: "billBalance", headerText: 'Bill Balance', format: "{0:n2}", textAlign: ej.TextAlign.Right, width: 40 },
                    { field: "amountPaid", headerText: "Amount Paid", format: "{0:n2}", textAlign: ej.TextAlign.Right, width: 45 },

                ]
            });


        },

        createCreditMemo() {

            this.payload = {};

            this.$v.$touch();

            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }

                }

                return;
            }

            this.isLoading = true;
            let academicYearInfo = this.getAcademicYearDetails(this.selectedAcademicYearId);
            let termInfo = this.getTermDetails(this.selectedTermId);
            let currencyInfo = this.getCurrencyDetails(this.selectedCurrencyId);

            this.payload.amountPaid = parseFloat(this.amountPaid);
            this.payload.academicYearId = this.selectedAcademicYearId;
            this.payload.termId = this.selectedTermId;
            this.payload.classId = this.selectedClassId;
            this.payload.studentId = this.selectedStudentId;
            this.payload.modeOfPayment = this.modeOfPayment;
            this.payload.paymentDate = this.paymentDate;
            this.payload.currencyId = this.selectedCurrencyId;
            this.payload.chequeNo = this.chequeNo;
            this.payload.billId = this.emptyGuid;
            this.payload.billNo = '';
            this.payload.receiptNo = this.receiptNo;
            this.payload.paymentDescription = 'Credit Memo/Advanced Payment(' + academicYearInfo.name + "-" + termInfo.name + ')';
            this.payload.isCreditMemo = true;
            this.payload.isPosted = true;

            axios.post('/BillsAndPayments/CreateCreditMemo', this.payload).then(response => {

                this.toastHandler('', 'Payment successful.');
                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Payment failed.');
                this.isLoading = false;

            });

        },

        getStudentBills() {

            if (this.selectedStudentId) {

                axios.get('/BillsAndPayments/ListStudentBills?id=' + this.selectedStudentId).then(response => {


                    response.data.forEach((v) => {
                        v.billDate = this.formatShortDate(v.billDate);
                        v.acaTerm = v.academicYearName + '-' + v.termName;
                    });

                    this.billData = response.data;
                    this.renderStudentBills(response.data);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get student bills.');

                });

            }
            
        },

        getCurrency() {

            axios.get('/SharedResource/GetCurrencys').then(response => {

                this.currencyList = response.data;
                if (this.currencyList.length) {

                    this.selectedCurrencyId = this.currencyList[0].id;
                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to get currency.');

            });
        },

        renderStudentBills(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                pageSettings: { pageSize: 10 },
                isResponsive: true,
                allowGrouping: true,
                allowSorting: true,
                enableDropAreaAnimation: true,
                sortSettings: { sortedColumns: [{ field: "acaTerm", direction: "ascending" }] },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.PrintGrid] },
                summaryRows: [
                    { title: "Balance Due", summaryColumns: [{ summaryType: ej.Grid.SummaryType.Sum, displayColumn: "billBalance", dataMember: "billBalance", format: "{0:n2}" }] },
                ],
                columns: [
                    { field: "id", headerText: 'Id', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "classId", headerText: 'ClassId', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "studentId", headerText: 'StudentId', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "studentIdentifier", headerText: 'StudentID', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "billId", headerText: 'BillId', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "billTypeId", headerText: 'billTypeId', visible: false, textAlign: ej.TextAlign.Left, width: 10 },
                    { field: "acaTerm", headerText: 'Academic Year/Term', textAlign: ej.TextAlign.Left, width: 60 },
                    { field: "className", headerText: 'Class', textAlign: ej.TextAlign.Left, width: 55 },
                    { field: "billNo", headerText: 'Bill No.', textAlign: ej.TextAlign.Left, width: 35 },
                    { field: "billDate", headerText: 'Bill Date', textAlign: ej.TextAlign.Left, width: 35 },
                    { field: "billAmount", headerText: 'Bill Amount', format: "{0:n2}", textAlign: ej.TextAlign.Right, width: 40 },
                    { field: "billBalance", headerText: 'Bill Balance', format: "{0:n2}", textAlign: ej.TextAlign.Right, width: 40 },
                    { field: "description", headerText: 'Description', textAlign: ej.TextAlign.Left, width: 80 },

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

        getTermDetails(id) {

            return this.termList.find(a => a.id === id);
        },
        getCurrencyDetails(id) {

            return this.currencyList.find(a => a.id === id);
        },

        getSchClasses() {

            axios.get('/schsetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getStudents() {

            axios.get('/Students/GetStudentByClass?id=' + this.selectedClassId).then(response => {

                this.studentList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get students.');

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
        },

        displayChequeInput() {

            return this.modeOfPayment == 'Cheque';
        }
    },
    watch: {

        selectedAcademicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }

        },

        selectedClassId(val) {

            this.getStudents();
        },

         
    },
    created() {

        this.getAcademicYears();
        this.getSchClasses();
        this.getCurrency();
    },
});