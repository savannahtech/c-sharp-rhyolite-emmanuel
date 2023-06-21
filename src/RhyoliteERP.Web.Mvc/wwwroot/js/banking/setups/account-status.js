
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/BankingSetups/GetAccountStatus').then(response => {

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
                    { field: "name", headerText: "Account Status", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveAccountStatus(args.data);
                },
                endEdit: (args) => {

                    this.updateAccountStatus(args.data);
                },
                endDelete: (args) => {

                    this.delAccountStatus(args.data.id);
                },
            });

        },

        saveAccountStatus(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/BankingSetups/CreateAccountStatus', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Account status saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save account status.');

            });
        },

        updateAccountStatus(payload) {

            axios.post('/BankingSetups/UpdateAccountStatus', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Account status updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update account status.');

            });
        },

        delAccountStatus(id) {

            axios.get('/BankingSetups/DelAccountStatus?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Account status deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete account status.');

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