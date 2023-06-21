 

var vue = new Vue({
    el: '#app',
    data: {

        isLoading: false,
        invoiceList: [],
        pageNo: 1,
        pageSize: 10,
        totalCount: 0,
        totalPages: 0,
        lowerBound: 0,
        upperBound: 0,
    },

    methods: {
        
        getInvoices() {

            axios.get(`/Manage/GetInvoices?pageNo=${this.pageNo}&pageSize=${this.pageSize}`).then(response => {

                this.invoiceList = response.data.result.data;
                this.totalCount = response.data.result.totalCount;
                this.totalPages = response.data.result.totalPages;
                this.lowerBound = response.data.result.lowerBound;
                this.upperBound = response.data.result.upperBound;

            }).catch(err => {

                this.toastHandler('error', '');

            });
        },

        downloadAsPdf(invoice) {

            console.log(invoice)
        },

        settleInvoice(invoice) {

            console.log(invoice)
        },

        onPageChanged(page) {

            this.pageNo = page;
            this.getInvoices();
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
    filters: {

        formatShortDate(dateInput) {

            return moment(dateInput).format("DD-MMM-YYYY");
        },
        currency: function (value) {
            return numeral(value).format('0,0.00');
        },
        number: function (value) {
            return numeral(value).format('0,0');
        },
    },
    computed: {

        pageList() {

            let counter = 1;
            let initPage = 1;
            const currentPage = this.pageNo;
            const pages = this.totalPages;

            if (currentPage > 9 && pages > 10) {
                initPage = currentPage - 5;
            }

            const pageNumbers = [];

            for (let i = initPage; i <= pages; i++) {
                pageNumbers.push(i);
                counter++;
                if (counter > 10) {
                    break;
                }
            }

            return pageNumbers;

        }

    },
    created() {

        this.getInvoices();

    },
});