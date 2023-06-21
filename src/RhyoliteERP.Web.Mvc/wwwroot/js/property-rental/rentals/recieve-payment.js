Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        residentAccountList: [],
        ledgerAccountList: [],
        tenantList: [],
        residentAccountId: '',
        revenueAccountId: '',
        amount: null,
        paymentMethod: 'Cash',
        recievedFromId: '',
        memo: '',
        attachmentFileUrl: ''
        
    },
    validations() {

        return {

            residentAccountId: {
                required
            },

            recievedFromId: {
                required
            },

            amount: {
                required
            },

            
        }


    },
    methods: {
        
        getResidentAccounts() {

            axios.get('/Rentals/GetResidentAccounts').then(response => {

                this.residentAccountList = response.data;

                $("#residentAccountId").ejDropDownList({
                    dataSource: response.data.sort((a, b) => a.accountCaption.localeCompare(b.accountCaption)),
                    watermarkText: "Select Account",
                    width: "100%",
                    fields: { id: "id", text: "accountCaption", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.residentAccountId = args.value;
                    }

                });


            }).catch(err => {

                //this.toastHandler('error', 'Unable to get applicants.');

            });
        },
         
        getLedgerAccounts() {

            axios.get('/LedgerSetups/GetLedgerAccounts').then(response => {

                this.ledgerAccountList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get ledger accounts.');

            });
        },

        getUnitTenants(leasedPropertyId, leasedPropertyUnitId) {

            axios.get(`/Rentals/GetUnitTenants?propertyId=${leasedPropertyId}&propertyUnitId=${leasedPropertyUnitId}`).then(response => {

                response.data.forEach((v) => {

                    v.tenantName = `${v.firstName} ${v.lastName}`;

                });

                this.tenantList = response.data;

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get applicants.');

            });

        },

        getResidentAccountDetails(id) {

            return this.residentAccountList.find(a => a.id === id);
        },

        recievePayment() {


            this.$v.$touch();

            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }
                    1
                }

                return;
            }  

            this.isLoading = true;

            let payload = {};

            payload.residentAccountId = this.residentAccountId;
            payload.amount = parseFloat(this.amount);
            payload.paymentMethod = this.paymentMethod;
            payload.recievedFromId = this.recievedFromId;

            let tenantInfo = this.getTenantDetails(this.recievedFromId);

            if (tenantInfo) {

                payload.recievedFromName = tenantInfo.tenantName;
            }

            payload.memo = this.memo;
            payload.attachmentFileUrl = this.attachmentFileUrl;
            payload.revenueAccountId = !this.revenueAccountId ? this.emptyGuid : this.revenueAccountId;


            axios.post('/Rentals/RecieveLeasePayment', payload).then(response => {

                this.toastHandler('', 'Payment recieved successfully.');

                this.isLoading = false;

                this.resetForm();

            }).catch(err => {

                this.toastHandler('error', 'Unable to create payment.');

                this.isLoading = false;

            });
        },

        resetForm() {

            this.recievedFromId = '';
            this.revenueAccountId = '';
            this.amount = null;

            this.$v.$reset();

        },

        getTenantDetails(id) {

            return this.tenantList.find(a => a.id === id);
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

        residentAccountId(val) {

            let residentAccountInfo = this.getResidentAccountDetails(val);

            if (residentAccountInfo) {

                this.getUnitTenants(residentAccountInfo.leasedPropertyId, residentAccountInfo.leasedPropertyUnitId);
            }
        },
    },
    created() {

        this.getResidentAccounts();
        this.getLedgerAccounts();
    },
});