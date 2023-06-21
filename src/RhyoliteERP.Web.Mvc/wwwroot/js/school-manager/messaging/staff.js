
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        recipientList: [],
        staffList: [],
        staffId: '',
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        allowedChars: /^[A-Za-z\sÅÄÖåäö]*$/,
        mentionDenotationChars: ["@", "["],
        isDirectMessage: false,
        subject: '',
        messagingChannel: 'sms',
        message: '',
        sendingText: 'Send',
    },

    methods: {

        getStaff() {

            axios.get('/Staff/GetStaff').then(response => {

                response.data.forEach((v) => {

                    v.staffName = v.otherName ? `${v.lastName} ${v.firstName} ${v.otherName}` : `${v.lastName} ${v.firstName}`
                });

                $("#staffId").ejDropDownList({
                    dataSource: response.data,
                    watermarkText: "Search Staff",
                    width: "100%",
                    fields: { id: "id", text: "staffName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.staffId = args.value;
                    }

                });

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff.');

            });
        },

        sendMessage() {

            if (this.isDirectMessage && !this.staffId) {

                this.toastHandler('warning', 'Select staff');
                return;

            }

            this.isLoading = true;
            this.sendingText = 'Sending...';
            this.payload = {};

            if (this.isDirectMessage && !this.staffId) {

                this.staffId = this.emptyGuid;
            }

            this.payload.subject = this.subject;
            this.payload.messagingChannel = this.messagingChannel;
            this.payload.message = this.message;
            this.payload.recipientId = this.staffId;
            this.payload.recipientType = 'staff';

            axios.post('/SchMessaging/SendBulkMessage', this.payload).then(response => {

                this.isLoading = false;
                this.sendingText = 'Send';

                this.toastHandler('', 'Messages sent.');

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

        this.getStaff();

    },
});