
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        enableUpload: false,
        fileInput: null
    },

    methods: {

        getSetupData() {

            axios.get('/schsetups/GetBillTemplate').then(response => {

                //this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to display bill template.');

            });
        },
        handleFileUpload(e) {

            let fileInput = e.target;
            this.fileInput =  fileInput.files[0]
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

        this.getSetupData();
 
    },
});