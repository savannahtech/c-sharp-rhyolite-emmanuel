
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        accountTypeList: []

    },

    methods: {

        getSetupData() {

            axios.get('/BankingSetups/GetBranches').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get branches');

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
                    { field: "name", headerText: "Branch Name", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "contactPerson", headerText: "Contact Person", validationRules: { required: true, minlength: 1 }, width: 85 },
                    { field: "contactNo", headerText: "Contact No", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "contactEmail", headerText: "Contact Email", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "branchManager", headerText: "Branch Manager", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "location", headerText: "Location", validationRules: { required: true, minlength: 1 }, width: 70 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveBranch(args.data);
                },
                endEdit: (args) => {

                    this.updateBranch(args.data);
                },
                endDelete: (args) => {

                    this.delBranch(args.data.id);
                },
            });

        },

        saveBranch(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/BankingSetups/CreateBranch', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Branch saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save branch.');

            });
        },

        updateBranch(payload) {

            axios.post('/BankingSetups/UpdateBranch', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Branch updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update branch.');

            });
        },

        delBranch(id) {

            axios.get('/BankingSetups/DelBranch?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Branch deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete branch.');

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