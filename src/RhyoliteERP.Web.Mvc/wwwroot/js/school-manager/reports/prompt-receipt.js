
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
        headerInfo: {},
        detailInfo: {},
        num: "Zero One Two Three Four Five Six Seven Eight Nine Ten Eleven Twelve Thirteen Fourteen Fifteen Sixteen Seventeen Eighteen Nineteen".split(" "),
        tens: "Twenty Thirty Forty Fifty Sixty Seventy Eighty Ninety".split(" "),
        reportApiBaseUrl: ''

    },

    methods: {


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
            
            if (wholeNumber < 100) return this.tens[~~(wholeNumber / 10) - 2] + (digit ? "-" + this.num[digit] + " Ghana Cedis" : "") +  pesewasInWords;
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
    
    
    created() {

        this.headerInfo = headerInfo;
        this.detailInfo = detailInfo;
        this.reportApiBaseUrl = reportApiBaseUrl;
         
        setTimeout(() => {

            let detailsList = [];
            this.detailInfo.payment.balanceDue = this.detailInfo.balanceDue;
            this.detailInfo.payment.amountInWords = this.numberToWords(this.detailInfo.payment.amountPaid);
            this.detailInfo.payment.paymentDate = this.formatShortDate(this.detailInfo.payment.paymentDate);
            detailsList.push(this.detailInfo.payment);
 
            this.renderReport(this.headerInfo, detailsList);
        }, 2500);
        

    },
});