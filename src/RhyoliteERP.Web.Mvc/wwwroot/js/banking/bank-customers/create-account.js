Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        isLoading: false,
        accountStatusList: [],
        accountTypeList: [],
        accountNumber: '',
        customerName: '',
        customerIdentifier: '',
        accountTypeId: '',
        accountStatusId: '',
        balance: 0

        
    },
    validations() {

        return {

            accountNumber: {
                required
            },
            firstName: {
                required
            },
          
            
        }


    },
    methods: {

        saveCustomer() {


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


            this.payload = {};
            this.isLoading = true;

        },
        
        generateAccountNumber() {

            this.accountNumber = Math.floor(Math.random() * 99999999999) + 10000000;

        },
        getCustomers() {

            axios.get('/BankCustomers/GetCustomers').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get customers.');

            });
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

        
    },
});