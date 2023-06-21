
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        moduleList: [

            { "Id": 100, "Name": "General Ledger" },
            { "Id": 200, "Name": "Payroll" },
            { "Id": 300, "Name": "School Manager" },
            { "Id": 400, "Name": "Stock Manager" },
            { "Id": 600, "Name": "Account Payables" },
            { "Id": 700, "Name": "Account Receivables" },
            { "Id": 800, "Name": "Assets Manager" },
            { "Id": 1300, "Name": "Property Rental" }
        ],

    },

    methods: {

        getSetupData() {

            axios.get('/SharedResource/GetSystemNumbers').then(response => {

                this.renderTable(response.data);


            }).catch(err => {

                this.toastHandler('error', 'Unable to get system numbers.');

            });
        },

        renderTable(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowAdding: true, allowEditing: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search,] },
                columns: [

                    { field: "id", headerText: "id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "moduleName", headerText: 'Module', validationRules: { required: true, }, editType: ej.Grid.EditingType.Dropdown, foreignKeyField: "Name", foreignKeyValue: "Name", textAlign: ej.TextAlign.Left, width: 65, dataSource: this.moduleList },
                    { field: "itemName", headerText: "System Number Item", validationRules: { required: true, minlength: 3 }, width: 65 },
                    { field: "prefix", headerText: "Prefix", width: 55, validationRules: { required: true, minlength: 1 }, },
                    { field: "suffix", headerText: "Suffix", width: 55, },
                    { field: "lastNo", headerText: "Last No#", format: "{0:n0}", editType: ej.Grid.EditingType.Numeric, editParams: { decimalPlaces: 0 }, validationRules: { required: true, }, width: 45 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.addSystemNumber(args.data);
                },
                endEdit: (args) => {

                    this.updateSystemNumber(args.data);
                },
                endDelete: (args) => {

                    this.delSystemNumber(args.data.id);
                },
            });

        },

        addSystemNumber(payload) {

            delete payload.id;
            delete payload.tenantId;

            if (!payload.prefix) {

                payload.prefix = '';
            }

            if (!payload.suffix) {

                payload.suffix = '';
            }

            if (!payload.lastNo) {

                payload.lastNo = 1;
            }

            axios.post('/SharedResource/CreateSystemNumber', payload).then(response => {

                this.getSetupData();

                if (response.data.code == 200) {
                    this.toastHandler('', 'System Number saved.');
                }
                else {
                    this.toastHandler('error', response.data.message);

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to save system number.');

            });
        },

        updateSystemNumber(payload) {

            if (!payload.prefix) {

                payload.prefix = '';
            }

            if (!payload.suffix) {

                payload.suffix = '';
            }

            if (!payload.lastNo) {

                payload.lastNo = 1;
            }
            axios.post('/SharedResource/UpdateSystemNumber', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'System Number updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update system number.');

            });
        },

        delSystemNumber(id) {

            axios.get('/SharedResource/DelSystemNumber?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'System Number deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete system number.');

            });
        },
        formatShortDate(dateInput) {

            return moment(dateInput).format("DD-MMM-YYYY");

        },
        uuid() {

            return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
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

    created() {

        this.getSetupData();
    },
});