
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        accountTypeList: [],
        isReceiptReady: false,
        isLoading: false,
        accountNo: '',
        customerName: '',
        amount: 0,
    },

    methods: {
 
        saveDeposit() {

            let payload = {};
            payload.amount = this.amount;
            payload.accountNo = this.accountNo;
            payload.customerId = this.customerId;

            axios.post('/BankingHall/SaveDeposit', payload).then(response => {


                this.toastHandler('', 'Deposit saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save deposit.');

            });
        },

        getCustomerDetails() {

            console.log('fired...');
        },

        printReceipt() {


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


    },
});