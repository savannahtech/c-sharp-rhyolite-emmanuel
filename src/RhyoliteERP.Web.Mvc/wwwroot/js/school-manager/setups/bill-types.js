
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        alertConfig: {
            type: '',
            alertMessage: null,
            showAlert: false,
            timeout: 5000
        },

    },

    methods: {

        getSetupData() {

            axios.get('/schsetups/GetBillTypes').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                console.log(err);
                this.toastHandler('error', 'Unable to get bill types.');

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
                    { field: "name", headerText: "Bill Type", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveBillType(args.data);
                },
                endEdit: (args) => {

                    this.updateBillType(args.data);
                },
                endDelete: (args) => {

                    this.delBillType(args.data.id);
                },
            });

        },

        saveBillType(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateBillType', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Bill type saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save bill type.');

            });
        },

        updateBillType(payload) {

            axios.post('/schsetups/UpdateBillType', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Bill type updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update bill type.');

            });
        },

        delBillType(id) {

            axios.get('/schsetups/DelBillType?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Bill type deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete bill type.');

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