
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        accountTypeList: []

    },

    methods: {

        getSetupData() {

            axios.get('/BankingSetups/GetAccountTypes').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get account Types');

            });
        },


        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Account Type", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveAccountType(args.data);
                },
                endEdit: (args) => {

                    this.updateAccountType(args.data);
                },
                endDelete: (args) => {

                    this.delAccountType(args.data.id);
                },
            });

        },

        saveAccountType(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/BankingSetups/CreateAccountType', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Account type saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save account type.');

            });
        },

        updateAccountType(payload) {

            axios.post('/BankingSetups/UpdateAccountType', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Account type updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update account type.');

            });
        },

        delAccountType(id) {

            axios.get('/BankingSetups/DelAccountType?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Account type deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete account type.');

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