 
Vue.component(VueQrcode.name, VueQrcode);

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        tenantId: null,
        syncFrequency: '',
        enableMobileAccess: '',
        signInMode: '',
        qr: '',
         
    },
    
    methods: {

        saveSyncSettings() {

            this.toastHandler('','Settings saved !');
        },
        initQrLogin() {

            console.log('initQrLogin');
        },
        generateQr() {
            return([1e7]+ -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
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

        this.qr = this.generateQr();
        this.initQrLogin();
    },
});