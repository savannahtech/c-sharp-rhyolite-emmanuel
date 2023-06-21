
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/LedgerSetups/GetImprestCategories').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                console.log(err);
                this.toastHandler('error', 'Unable to get imprest categories');

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
                    { field: "name", headerText: "Imprest Category", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveImprestCategory(args.data);
                },
                endEdit: (args) => {

                    this.updateImpressCategory(args.data);
                },
                endDelete: (args) => {

                    this.delImpressCategory(args.data.id);
                },
            });

        },

        saveImprestCategory(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/LedgerSetups/CreateImprestCategory', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Imprest category saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save imprest category.');

            });
        },

        updateImpressCategory(payload) {

            axios.post('/LedgerSetups/UpdateImprestCategory', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Imprest Category updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update imprest category.');

            });
        },

        delImpressCategory(id) {

            axios.get('/LedgerSetups/DelImprestCategory?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Imprest Category deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete imprest category.');

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