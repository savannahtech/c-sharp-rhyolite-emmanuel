
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        subject: '',
        messagingChannel: 'sms',
        message: '',
        sendingText: 'Send',
        uploadFileInput: null,
        uploadProgressValue: 0
    },

    methods: {
 
        onFileUpload(evt) {

            this.uploadFileInput = evt.target.files[0];
             
        },

        sendMessage() {

            this.isLoading = true;
            this.sendingText = 'Sending...';
           
            let formData = new FormData();
            formData.set('file', this.uploadFileInput);
            //formData.set('connectionId', this.progressHubConnection.connectionId);
            formData.set('subject', this.subject);
            formData.set('messagingChannel', this.messagingChannel);
            formData.set('message', this.message);
            formData.set('recipientId', this.emptyGuid);
            formData.set('recipientType', '');

            const config = {
                onUploadProgress: progressEvent => this.uploadProgressValue = ((progressEvent.loaded / progressEvent.total) * 100).toFixed(2),
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }

            axios.post('/RentalMessaging/SendMessagesFromExcel', formData, config ).then(response => {

                if (response.data.code == 200) {

                    this.toastHandler('', 'Messages sent.');
                }
                else {

                    this.toastHandler('error', response.data.message);

                }
                
                this.isLoading = false;
                this.sendingText = 'Send';


            }).catch(err => {

                this.isLoading = false;
                this.sendingText = 'Send';

                this.toastHandler('error', 'Unable to send message.');

            });

        },
         
        toastHandler(type, message = 'An unexpected error occurred.') {

            //display alert with 
            toastr.options = {
                closeButton: true,
                progressBar: true,
                showMethod: 'fadeIn',
                hideMethod: 'fadeOut',
                timeOut: 20000,
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