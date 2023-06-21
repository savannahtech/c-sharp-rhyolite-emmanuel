
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        accountTypeList: []

    },

    methods: {

        getSetupData() {

            axios.get('/BankingSetups/GetTransactionTypes').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get transaction types');

            });
        },


        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData.transactionTypes,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Transaction Type", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "streamId", headerText: "Account", editType: ej.Grid.EditingType.Dropdown, foreignKeyField: "id", foreignKeyValue: "name", dataSource: responseData.accounts, textAlign: ej.TextAlign.Left, width: 55 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveTransactionType(args.data);
                },
                endEdit: (args) => {

                    this.updateTransactionType(args.data);
                },
                endDelete: (args) => {

                    this.delTransactionType(args.data.id);
                },
            });

        },

        saveTransactionType(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/BankingSetups/CreateTransactionType', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Transaction types saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save transaction types.');

            });
        },

        updateTransactionType(payload) {

            axios.post('/BankingSetups/UpdateTransactionType', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Transaction types updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update transaction types.');

            });
        },

        delTransactionType(id) {

            axios.get('/BankingSetups/DelTransactionType?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Transaction types deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete transaction types.');

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

        this.getSetupData();

    },
});