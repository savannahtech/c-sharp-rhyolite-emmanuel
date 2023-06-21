
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/schsetups/GetSpecialDuties').then(response => {

                 
                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get special duties.');

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
                    { field: "name", headerText: "Special Duty", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveSpecialDuty(args.data);
                },
                endEdit: (args) => {

                    this.updateSpecialDuty(args.data);
                },
                endDelete: (args) => {

                    this.delSpecialDuty(args.data.id);
                },
            });

        },

        saveSpecialDuty(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateSpecialDuty', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Special duty saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save special duty.');

            });
        },

        updateSpecialDuty(payload) {

            axios.post('/schsetups/UpdateSpecialDuty', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Special duty updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update special duty.');

            });
        },

        delSpecialDuty(id) {

            axios.get('/schsetups/DelSpecialDuty?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Special duty deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete special duty.');

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