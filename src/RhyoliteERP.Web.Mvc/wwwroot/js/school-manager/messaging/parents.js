
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        isDirectMessage: false,
        parentId: '',
        smsTemplateList: [],
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        subject: '',
        messagingChannel: 'sms',
        message: '',
        tokenExpressionList: [
            { 'id': 1, 'expression': '[BillBalance]', 'description': 'Outstanding bill balance till date' },
            { 'id': 2, 'expression': '[BillAmount]', 'description': 'Current bill amount from for the current Academic year & term setup on the school profile.' },
            { 'id': 3, 'expression': '[BillNo]', 'description': 'Current bill number generated for the current Academic year & term setup on the school profile.' },
            { 'id': 4, 'expression': '[1stGuardianName]', 'description': 'Name of first guardian' },
            { 'id': 5, 'expression': '[2ndGuardianName]', 'description': 'Name of second guardian' },
            { 'id': 6, 'expression': '[StudentName]', 'description': 'Student full name' },
            { 'id': 7, 'expression': '[StaffName]', 'description': 'Staff full name' },
            { 'id': 8, 'expression': '[StudentID]', 'description': 'Student ID of student' },
            { 'id': 9, 'expression': '[ClassName]', 'description': 'Current class of student' },
        ],
        sendingText: 'Send',
        showTokenPopupHelper: false,
        showTemplatePopupHelper: false,
        isHelperInsertOperationComplete: false

    },

    methods: {

        getParents() {

            axios.get('/Parents/GetParents').then(response => {
                 
                response.data.forEach((v) => {

                    v.parentName = v.secondGuardianName ? `${v.firstGuardianName} & ${v.secondGuardianName}` : `${v.firstGuardianName}`
                });

                $("#parentId").ejDropDownList({
                    dataSource: response.data,
                    watermarkText: "Search Parent",
                    width: "100%",
                    fields: { id: "id", text: "parentName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.parentId = args.value;
                    }

                });

            }).catch(err => {

                this.toastHandler('error', 'Unable to get parents.');

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
                    { field: "firstGuardianName", headerText: "1st Guardian Name", validationRules: { required: true, minlength: 1 }, width: 65, allowEditing: false },
                    { field: "secondGuardianName", headerText: "2nd Guardian Name", validationRules: { required: true, minlength: 1 }, width: 65, },
                    { field: "firstGuardianPhoneNo", headerText: "1st Guardian Phone#", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "firstGuardianEmail", headerText: "1st Guardian Email", validationRules: { date: true }, width: 55, },
                    { field: "secondGuardianEmail", headerText: "2nd Guardian Email", validationRules: { date: true }, width: 55, },

                ]
                
            });

        },

        sendMessage() {

            if (this.isDirectMessage && !this.parentId) {

                this.toastHandler('warning', 'Select parent');
                return;

            }

            this.isLoading = true;
            this.sendingText = 'Sending...';

            this.payload = {};

            if (this.isDirectMessage) {

                this.parentId = this.emptyGuid;
            }

            this.payload.subject = this.subject;
            this.payload.messagingChannel = this.messagingChannel;
            this.payload.message = this.message;
            this.payload.recipientId = this.parentId;
            this.payload.recipientType = 'parent';
            
            

            axios.post('/SchMessaging/SendBulkMessage', this.payload).then(response => {

                this.isLoading = false;
                this.sendingText = 'Send';

                this.toastHandler('', 'Messages queued to be sent.');

            }).catch(err => {

                this.isLoading = false;
                this.sendingText = 'Send';

                this.toastHandler('error', 'Unable to send messages.');

            });

        },

        setRecipientsData() {
 
            var obj = $("#dataTable").ejGrid("instance"), rec = [];
            this.selectedParentList = [];
             var check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
             if (check.length) {
                 for (var pageInx = 0; pageInx < check.length; pageInx++) {
                     if (!ej.isNullOrUndefined(check[pageInx]))
                         rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                 }
             }

            this.selectedParentList = rec;

        },

        selectTemplate(template) {

            this.message = template.messageContent;

            this.message = this.message.replace('@','');

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

            console.log('hide all.');
            setTimeout(() => {

                this.showTokenPopupHelper = false;
                this.showTemplatePopupHelper = false;
                this.isHelperInsertOperationComplete = true;


            },260);
            
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

        this.getParents();
        this.getTemplates();

    },
});