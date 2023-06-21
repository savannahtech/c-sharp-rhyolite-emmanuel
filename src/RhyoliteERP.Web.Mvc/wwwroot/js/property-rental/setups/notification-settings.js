
var vue = new Vue({
    el: '#app',
    data: {

        isLoading: false,
        smsTemplateList: [],
        pageNo: 1,
        pageSize: 1000000,
        totalCount: 0,
        totalPages: 0,
        templateId: null,
        tenantId: 0,
        sendPaymentSmsAlert: false,
        sendPaymentEmailAlert: false,
        sendRentExpiryReminderSmsAlert: false,
        sendRentExpiryReminderEmailAlert: false,
        sendRentOnboardSmsAlert: false,
        sendRentOnboardEmailAlert: false,
        sendLeaseRenewalSmsAlert: false,
        sendLeaseRenewalEmailAlert: false,
        sendTenantBirthdaySmsAlert: false,
        tenantBirthdaySmsAlertTemplateId: '',
        smsLowBalanceLimit: 0,
        sendSmsLowBalanceLimitAlert: false,
        notificationSettingsId: '',

    },

    methods: {

        getTemplates() {

            axios.get(`/RentalMessaging/GetMessageTemplates?pageNo=${this.pageNo}&pageSize=${this.pageSize}`).then(response => {

                this.smsTemplateList =  response.data.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get templates.');

            });
        },

        getNotificationSettings() {

            axios.get('/RentalSetups/GetNotificationSettings').then(response => {


                if (response.data) {

                    let notificationSettings = response.data;

                    this.sendPaymentSmsAlert = notificationSettings.sendPaymentSmsAlert;
                    this.sendPaymentEmailAlert = notificationSettings.sendPaymentEmailAlert;
                    this.sendRentExpiryReminderSmsAlert = notificationSettings.sendRentExpiryReminderSmsAlert;
                    this.sendRentExpiryReminderEmailAlert = notificationSettings.sendRentExpiryReminderEmailAlert;
                    this.sendRentOnboardSmsAlert = notificationSettings.sendRentOnboardSmsAlert;
                    this.sendRentOnboardEmailAlert = notificationSettings.sendRentOnboardEmailAlert;
                    this.sendLeaseRenewalSmsAlert = notificationSettings.sendLeaseRenewalSmsAlert;
                    this.sendLeaseRenewalEmailAlert = notificationSettings.sendLeaseRenewalEmailAlert;
                    this.sendTenantBirthdaySmsAlert = notificationSettings.sendTenantBirthdaySmsAlert;
                    this.tenantBirthdaySmsAlertTemplateId = notificationSettings.tenantBirthdaySmsAlertTemplateId;
                    this.smsLowBalanceLimit = notificationSettings.smsLowBalanceLimit;
                    this.sendSmsLowBalanceLimitAlert = notificationSettings.sendSmsLowBalanceLimitAlert;
                    this.notificationSettingsId = notificationSettings.id;
                    this.tenantId = notificationSettings.tenantId;

                }




            }).catch(err => {

                //this.toastHandler('error', 'Unable to get templates.');

            });
        },

        saveNotificationsSettings() {

            let payload = {};

            this.isLoading = true;

            payload.sendPaymentSmsAlert = this.sendPaymentSmsAlert;
            payload.sendPaymentEmailAlert = this.messageSubject;
            payload.sendRentExpiryReminderSmsAlert = this.messageContent;
            payload.sendRentExpiryReminderEmailAlert = this.messageContent;
            payload.sendRentOnboardSmsAlert = this.sendRentOnboardSmsAlert;
            payload.sendRentOnboardEmailAlert = this.sendRentOnboardEmailAlert;
            payload.sendLeaseRenewalSmsAlert = this.sendLeaseRenewalSmsAlert;
            payload.sendLeaseRenewalEmailAlert = this.sendLeaseRenewalEmailAlert;
            payload.sendTenantBirthdaySmsAlert = this.sendTenantBirthdaySmsAlert;
            payload.tenantBirthdaySmsAlertTemplateId = this.messageContent;
            payload.smsLowBalanceLimit = this.smsLowBalanceLimit;
            payload.sendSmsLowBalanceLimitAlert = this.sendSmsLowBalanceLimitAlert;

            if (this.notificationSettingsId != null) {

                payload.id = this.notificationSettingsId;
                payload.tenantId = this.tenantId;

                axios.post('/RentalSetups/UpdateNotificationSettings', payload).then(response => {

                    this.toastHandler('', 'Notification settings updated.');
                    this.isLoading = false;


                }).catch(err => {

                    this.toastHandler('error', 'Unable to update notification settings.');
                    this.isLoading = false;

                });

            } else {

                axios.post('/RentalMessaging/CreateNotificationSettings', payload).then(response => {

                    this.getTemplates();

                    this.toastHandler('', 'Notification settings saved.');

                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save notification settings.');
                    this.isLoading = false;


                });
            }

           
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

        this.getNotificationSettings();
        this.getTemplates();
    },
});