
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        academicYearList: [],
        termList: [],
        schClasses: [],
        studentList: [],
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        selectedStudentId: '',
        studentID: '',
        studentName: '',
        schClasses: [],
        paymentList: [],
        num: "Zero One Two Three Four Five Six Seven Eight Nine Ten Eleven Twelve Thirteen Fourteen Fifteen Sixteen Seventeen Eighteen Nineteen".split(" "),
        tens: "Twenty Thirty Forty Fifty Sixty Seventy Eighty Ninety".split(" "),
        reportApiBaseUrl: ''
    },

    methods: {

        getData(receiptNo) {


            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedStudentId && this.selectedClassId && receiptNo) {

                axios.get('/SchReports/GetPaymentReceipt?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&studentId=' + this.selectedStudentId + '&receiptNo=' + receiptNo).then(response => {

                    let detailsList = [];
                    response.data.details.payment.balanceDue = response.data.details.balanceDue;
                    response.data.details.payment.amountInWords = this.numberToWords(response.data.details.payment.amountPaid);
                    response.data.details.payment.paymentDate = this.formatShortDate(response.data.details.payment.paymentDate);
                    detailsList.push(response.data.details.payment);

                    console.log(detailsList);
                     
                    this.renderReport(response.data.header, detailsList);


                }).catch(err => {

                    this.toastHandler('error', 'Unable to get data.');

                });


            }
             
        },

        getPayments() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedStudentId) {


                axios.get('/SchReports/GetStudentPayments?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&studentId=' + this.selectedStudentId).then(response => {

                    if (response.data.length) {

                        this.studentID = response.data[0].studentIdentifier;
                        this.studentName = response.data[0].studentName;
                    }

                    this.paymentList = response.data;
                    $('#receiptModal').modal('show');


                }).catch(err => {

                    this.toastHandler('error', 'Unable to get data.');

                });


            }

        },
        selectPayment(payment) {

            this.getData(payment.receiptNo);
            $('#receiptModal').modal('hide');

        },

        renderReport(header, details) {

            $("#reportView").empty();

            $("#reportView").boldReportViewer({

                reportServiceUrl: this.reportApiBaseUrl,
                processingMode: ej.ReportViewer.ProcessingMode.Local,
                reportPath: 'https://res.cloudinary.com/rhyoliteprime/raw/upload/v1634727314/reportapi/sm/PaymentReceipt.rdl',
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

        getClassDetails(id) {
            return this.classList.find(a => a.id === id);
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

        numberToWords(n) {

            let pesewasInWords = '';
            let decimalRemainder = n % 1;
            let wholeNumber = n - decimalRemainder;

            if (decimalRemainder > 0) {

                let d1 = decimalRemainder.toString();
                let d2 = d1.split('.')[1].substring(0, 2);
                if (d2.charAt(0) != '0' && d2.length == 1) {

                    d2 = d2 + '0';
                }

                let decimalNumber = parseInt(d2);

                var pesewasDigit = decimalNumber % 10;

                pesewasInWords = " and " + this.tens[~~(decimalNumber / 10) - 2] + (pesewasDigit ? "-" + this.num[pesewasDigit] : "") + " Pesewas";

            }

            if (wholeNumber < 20) return this.num[wholeNumber] + " Ghana Cedis";
            var digit = wholeNumber % 10;

            if (wholeNumber < 100) return this.tens[~~(wholeNumber / 10) - 2] + (digit ? "-" + this.num[digit] + " Ghana Cedis" : "") + pesewasInWords;
            if (wholeNumber < 1000) return this.num[~~(wholeNumber / 100)] + " Hundred" + (wholeNumber % 100 == 0 ? + " Ghana Cedis" : " and " + this.numberToWords(wholeNumber % 100) + " Ghana Cedis") + pesewasInWords;

            return this.numberToWords(~~(wholeNumber / 1000)) + " Thousand" + (wholeNumber % 1000 != 0 ? " " + this.numberToWords(wholeNumber % 1000) : "") + pesewasInWords;
            
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
    filters: {

        currency: function (value) {
            return numeral(value).format('0,0.00');
        },
        number: function (value) {
            return numeral(value).format('0,0');
        },
         
         
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
        this.reportApiBaseUrl = reportApiBaseUrl;

    },
});