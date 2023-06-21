Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isLoading: false,
        payload: {},
        businessName: '',
        primaryPhone: '',
        primaryEmail: '',
        city: '',
        locationOrAddress: '',
    },
    validations() {

        return {

            businessName: {
                required
            },
            primaryPhone: {
                required
            },
            primaryEmail: {
                required
            },
            city: {
                required
            },
            locationOrAddress: {
                required
            },
        }

    },
    methods: {

        register() {

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
            let payload = {};

            payload.businessName = this.businessName;
            payload.primaryPhone = this.primaryPhone;
            payload.primaryEmail = this.primaryEmail;
            payload.city = this.city;
            payload.locationOrAddress = this.locationOrAddress;

          
            axios.post('/Account/SendInvitation', payload).then(response => {

                this.isLoading = false;
                this.resetForm();

                this.toastHandler('', 'Registration successful.');

            }).catch(err => {

                this.isLoading = false;

                this.toastHandler('error', 'Registration failed.');

            });
            
        },
        resetForm() {

            this.businessName = '';
            this.primaryPhone = '';
            this.primaryEmail = '';
            this.city = '';
            this.locationOrAddress = '';

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
    watch: {

        
    },
    created() {

        
    },
});