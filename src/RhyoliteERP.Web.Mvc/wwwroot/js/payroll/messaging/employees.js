
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        recipientList: [],
        staffList: [],
        employeeId: '',
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        allowedChars: /^[A-Za-z\sÅÄÖåäö]*$/,
        mentionDenotationChars: ["@", "["],
        tokenExpressionList: [
            { 'id': 1, 'expression': '[LoanBalance]', 'description': 'Current loan balance till date' },
            { 'id': 2, 'expression': '[LoanType]', 'description': 'Type of loan requested by employee' },
            { 'id': 3, 'expression': '[AccountNo]', 'description': 'Masked Account Number of Employee' },
            { 'id': 4, 'expression': '[BankName]', 'description': 'Current employee bank' },
            { 'id': 5, 'expression': '[BankBranchName]', 'description': 'Current employee bank branch' },
            { 'id': 6, 'expression': '[PayType]', 'description': 'Mode of salary payment for employee' },
            { 'id': 7, 'expression': '[SocialSecurityNo]', 'description': 'Employee Social Security Number' },
            { 'id': 8, 'expression': '[EmployeeName]', 'description': 'Employee full name' },
            { 'id': 9, 'expression': '[EmployeeID]', 'description': 'Employee ID' },
            { 'id': 10, 'expression': '[EmployeeCategory]', 'description': 'Current Employee Category' },
            { 'id': 11, 'expression': '[EmployeeDepartment]', 'description': 'Current Employee Department' },
        ],
        isDirectMessage: false,
        subject: '',
        messagingChannel: 'sms',
        message: '',
        sendingText: 'Send',
        showTokenPopupHelper: false,
        showTemplatePopupHelper: false,
        isHelperInsertOperationComplete: false
    },

    methods: {

        getEmployees() {

            axios.get('/PayrollDataEntry/GetEmployees').then(response => {

                response.data.forEach(v => {

                    v.employeeName = v.otherName == '' || null ? `${v.lastName} ${v.firstName} - ${v.employeeIdentifier}` : `${v.lastName} ${v.firstName} ${v.otherName} - ${v.employeeIdentifier}`

                });

                $("#employeeId").ejDropDownList({
                    dataSource: response.data.sort((a, b) => a.lastName.localeCompare(b.lastName)),
                    watermarkText: "Search Employee",
                    width: "100%",
                    fields: { id: "id", text: "employeeName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.employeeId = args.value;
                    }

                });

            }).catch(err => {

                this.toastHandler('error', 'Unable to get employees.');

            });
        },

        sendMessage() {

            if (this.isDirectMessage && !this.employeeId) {

                this.toastHandler('warning', 'Select an employee');
                return;
            }

            this.isLoading = true;
            this.sendingText = 'Sending...';
            this.payload = {};

            if (!this.isDirectMessage) {

                this.employeeId = this.emptyGuid;
            }

            this.payload.subject = this.subject;
            this.payload.messagingChannel = this.messagingChannel;
            this.payload.message = this.message;
            this.payload.recipientId = this.employeeId;
            this.payload.recipientType = 'employee';

            axios.post('/PayrollMessaging/SendBulkMessage', this.payload).then(response => {

                this.isLoading = false;
                this.sendingText = 'Send';

                this.toastHandler('', 'Messages sent.');

            }).catch(err => {

                this.isLoading = false;
                this.sendingText = 'Send';

                this.toastHandler('error', 'Unable to send message.');

            });

        },

        selectTemplate(template) {

            this.message = template.messageContent;

            this.message = this.message.replace('@', '');

            $('#messageTemplates').modal('hide');

        },
        selectToken(tokenText) {

            let textarea = this.$refs.message;
            let cursorPosition = textarea.selectionStart;

            let text = this.message;
            let p1 = text.substring(0, cursorPosition);
            let p2 = text.substring(cursorPosition);

            let p1Array = p1.split(' ');
            let p2Array = p2.split(' ');

            let filteredPart1 = p1Array.filter(a => a != '[' && a != '');
            let filteredPart2 = p2Array.filter(a => a != '[' && a != '');

            this.message = filteredPart1.join(' ').toString() + ' ' + tokenText + ' ' + filteredPart2.join(' ').toString();

            this.$refs.message.focus();

            this.showTokenPopupHelper = false;
            this.isHelperInsertOperationComplete = true;

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

        onMessageUpdated() {

            this.showTokenPopupHelper = this.message.includes('[');

            if (this.message.includes('@')) {

                $('#messageTemplates').modal('show');

            }

            this.showTemplatePopupHelper = this.message.includes('@');
            this.isHelperInsertOperationComplete = false;

        },
        hideSuggestionPopups() {

            this.onMessageUpdated();

            setTimeout(() => {

                this.showTokenPopupHelper = false;
                this.showTemplatePopupHelper = false;
                this.isHelperInsertOperationComplete = true;


            }, 260);

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

        this.getEmployees();
        this.getTemplates();
    },
});