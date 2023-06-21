Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        id: null,
        tenantId: null,
        currentPassword: '',
        newPassword: '',
        confirmNewPassword: '',
        
 

    },
    validations() {

        return {

            currentPassword: {
                required
            },
            newPassword: {
                required
            },
            confirmNewPassword: {
                required
            },

        }

    },
    methods: {

        updatePassword() {

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

            this.payload.currentPassword = this.currentPassword;
            this.payload.newPassword = this.newPassword;

            axios.post('/Manage/ChangePassword', this.payload).then((response) => {

                this.isLoading = false;
                this.toastHandler('', 'Password updated.');

            }).catch((error) => {

                //console.log(error.response);
                this.isLoading = false;
                this.toastHandler('error', 'Unable to update password.');

            });

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

    },
    watch: {


    },

    created() {
         
    },
});