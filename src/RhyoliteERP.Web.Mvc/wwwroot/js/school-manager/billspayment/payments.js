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
        chequeNo: '',
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        siteBaseUrl: '',
        receiptList: []

    },
    validations() {

        return {

            amountPaid: {
                required
            },

        }


    },
    methods: {


        printPaymentReceipt() {

            for (let i = 0; i <= this.receiptList.length; i++) {
                
                window.open(`${this.siteBaseUrl}BillsAndPayments/Receipt?id=${this.receiptList[i]}`);

            }
        },

        applyFee() {

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

            let amountPayed = this.amountPaid;
            this.paymentsData = this.billData;

            if (this.paymentsData.length && this.amountPaid > 0) {

                let tempArray = [];

                for (var i = 0; i < this.paymentsData.length; i++) {

                    var payment = this.paymentsData[i];
                    var newBalance = payment.billBalance - amountPayed;

                    if (newBalance < 0) {

                        //means overpayment for next iteration
                        var remAmount = amountPayed - payment.billBalance;

                        payment.amountPaid = parseFloat(payment.billBalance);

                        if (this.paymentsData.length - 1 == i) {
                            payment.billBalance = parseFloat(-1 * remAmount) ;

                        }
                        else {
                            payment.billBalance = 0;

                        }
                        amountPayed = remAmount;
                    }
                    else if (newBalance == 0) {

                        //update and break
                        var remAmount = amountPayed - payment.billBalance;
                        payment.amountPaid = parseFloat(payment.billBalance);
                        payment.billBalance = 0;
                        amountPayed = remAmount;

                    }
                    else if (newBalance > 0) {

                        payment.billBalance = parseFloat(newBalance);
                        payment.amountPaid = parseFloat(amountPayed);
                        amountPayed = 0;

                    }

                    tempArray.push(payment);

                }

                this.billResidue = tempArray;


                this.renderPaymentTable();

                tempArray.forEach( (v) => {

                    v.modeOfPayment = this.modeOfPayment;
                    v.paymentDate = this.paymentDate;
                    v.currencyId = this.selectedCurrencyId;
                    v.currencyBuyRate = currencyInfo.buyRate;
                    v.currencySellRate = currencyInfo.sellRate;
                    v.paymentDescription = "Payment as School Fees For " + academicYearInfo.name + "-" + termInfo.name;
                    v.receiptNo = this.receiptNo;
                    v.academicYearId = this.selectedAcademicYearId;
                    v.termId = this.selectedTermId;
                    v.isCreditMemo = "False";
                    v.billId = v.id;
                    v.chequeNo = this.chequeNo;

                });

               
                //save Payment record to DB
                this.savePayment(tempArray);

                if (amountPayed > 0) {

                    this.payload.academicYearId = this.selectedAcademicYearId;
                    this.payload.termId = this.selectedTermId;
                    this.payload.classId = this.selectedClassId;
                    this.payload.studentId = this.selectedStudentId;
                    this.payload.modeOfPayment = this.modeOfPayment;
                    this.payload.paymentDate = this.paymentDate;
                    this.payload.currencyId = this.selectedCurrencyId;
                    this.payload.paymentDescription = "Payment as School Fees For " + academicYearInfo.name + "-" + termInfo.name;
                    this.payload.receiptNo = this.receiptNo;
                    this.payload.isCreditMemo = true;
                    this.payload.billId = tempArray[0].billId;
                    this.payload.chequeNo = this.chequeNo;
                    this.payload.billId = tempArray[0].billId;
                    this.payload.billNo = tempArray[0].billNo;
                    this.payload.amountPaid = amountPayed;
                    

                    //create credit memo...

                    axios.post('/BillsAndPayments/CreateCreditMemo', this.payload).then(response => {

                        this.isLoading = false;

                    }).catch(err => {

                        this.isLoading = false;

                        //this.toastHandler('error', 'Payment failed.');

                    });

                }

            }

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

        savePayment(paymentList) {

            if (paymentList.length) {

                
                axios.post('/BillsAndPayments/PayStudentBills', paymentList).then(response => {

                    this.toastHandler('', 'Payment successful.');
                    this.isLoading = false;
                    this.receiptList = response.data;
                    this.isReceiptReady = true;
                    this.resetReceiptState();

                }).catch(err => {

                    this.toastHandler('error', 'Payment failed.');
                    this.isLoading = false;

                });

            }

        },

        resetReceiptState() {

            setTimeout(() => {

                this.isReceiptReady = false;
                this.amountPaid = null;

            }, 15000);
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

        selectedStudentId(val) {

            this.getStudentBills();
        }
    },
    created() {

        this.getAcademicYears();
        this.getSchClasses();
        this.getCurrency();
        this.siteBaseUrl = siteBaseUrl;
    },
});