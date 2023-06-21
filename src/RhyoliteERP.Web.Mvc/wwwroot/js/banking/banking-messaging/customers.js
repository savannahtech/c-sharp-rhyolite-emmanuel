
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        recipientList: [],
        selectedCustomersList: [],
        smsTemplateList: [],
        subject: '',
        messagingChannel: 'sms',
        message: '',
        tokenExpressionList: [
            { 'id': 1, 'expression': '[LoanBalance]', 'description': 'Outstanding balance on recent loan' },
            { 'id': 6, 'expression': '[CustomerName]', 'description': 'Customer full name' },
            { 'id': 6, 'expression': '[CustomerFirstName]', 'description': 'Customer first name' },
            { 'id': 7, 'expression': '[TellerName]', 'description': 'Teller full name' },
            { 'id': 8, 'expression': '[TellerFirstName]', 'description': 'Teller first name' },
        ],

    },

    methods: {

        getCustomers() {

            axios.get('/BankCustomers/GetBankCustomers').then(response => {
                 
                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get customers.');

            });
        },


        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Search, ej.Grid.ToolBarItems.Print] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { type: "checkbox", width: 20 },
                    { field: "firstName", headerText: "First Name", validationRules: { required: true, minlength: 1 }, width: 65, allowEditing: false },
                    { field: "lastName", headerText: "Last Name", validationRules: { required: true, minlength: 1 }, width: 65, allowEditing: false },
                    { field: "otherName", headerText: "Other Name", validationRules: { required: true, minlength: 1 }, width: 65, allowEditing: false },
                    { field: "primaryPhoneNo", headerText: "Phone No", validationRules: { required: true, minlength: 1 }, width: 55, },

                ]
                
            });

        },

        sendMessage() {

            this.recipientList = [];
            this.isLoading = true;

            this.setRecipientsData();

            this.selectedCustomersList.forEach((v) => {
                
                this.recipientList.push(v.id);

            });

            this.payload = {};
            this.payload.subject = this.subject;
            this.payload.messagingChannel = this.messagingChannel;
            this.payload.message = this.message;
            this.payload.recipients = this.recipientList;
            this.payload.recipientType = 'client';

            if (this.recipientList) {

                axios.post('/BankingMessaging/SendBulkMessage', this.payload).then(response => {

                    this.isLoading = false;

                    this.toastHandler('', 'Messages queued to be sent.');

                }).catch(err => {

                    this.isLoading = false;

                    this.toastHandler('error', 'Unable to send messages.');

                });
            }

        },
        setRecipientsData() {
 
            var obj = $("#dataTable").ejGrid("instance"), rec = [];
            this.selectedCustomersList = [];
             var check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
             if (check.length) {
                 for (var pageInx = 0; pageInx < check.length; pageInx++) {
                     if (!ej.isNullOrUndefined(check[pageInx]))
                         rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                 }
             }

            this.selectedCustomersList = rec;

          },
        selectTemplate(template) {

            this.message = template.messageContent;

            $('#messageTemplates').modal('hide');

        },
        selectToken(tokenText) {

            let textarea = this.$refs.message;
            let cursorPosition = textarea.selectionStart;

            var text = this.message;
            var p1 = text.substring(0, cursorPosition);
            var p2 = text.substring(cursorPosition);
            this.message = p1 + ' ' + tokenText + ' ' + p2;

            this.$refs.message.focus();

            $('#tokens').modal('hide');


        },
        getRecords(pageInx, inx, proxy, rec) {

            if (inx.length) {
                for (var i = 0; i < inx.length; i++) {
                    var pageSize = proxy.model.pageSettings.pageSize;  //gets the page size of grid
                    var data = proxy.model.dataSource[pageInx * pageSize + inx[i]];
                    rec.push(data);     //pushing all the selected Records
                }
            }
            return rec;

        },
        getTemplates() {

            axios.get('/SchMessaging/GetMessageTemplates?pageNo=1&pageSize=1000000').then(response => {

                this.smsTemplateList = response.data.data;

                

            }).catch(err => {

                this.toastHandler('error', 'Unable to get templates.');

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

        this.getCustomers();
        this.getTemplates();

    },
});