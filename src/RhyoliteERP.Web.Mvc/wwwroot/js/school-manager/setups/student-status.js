
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

            axios.get('/schsetups/GetStatus').then(response => {

                 
                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get student/staff status.');

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
                    { field: "name", headerText: "Status", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "isDefault", headerText: "Is Default?", displayAsCheckbox: true, editType: ej.Grid.EditingType.Boolean, type: 'boolean', editParams: { showRoundedCorner: true }, width: 55 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveStatus(args.data);
                },
                endEdit: (args) => {

                    this.updateStatus(args.data);
                },
                endDelete: (args) => {

                    this.delStatus(args.data.id);
                },
            });

        },

        saveStatus(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateStatus', payload).then(response => {


                if (!response.data.isDefaultDuplicate && !response.data.isDuplicate) {
                     
                    this.toastHandler('', 'Student/Staff status saved.');
                     
                }
                else if (response.data.isDefaultDuplicate && response.data.isDuplicate) {
                    
                    this.toastHandler('warning', 'Only one status can be set to default at a time.');

                }
                else if (response.data.isDefaultDuplicate) {
                     
                    this.toastHandler('warning', 'Only one status can be set to default at a time.');

                }
                else if (response.data.isDuplicate) {

                    this.toastHandler('warning', 'Duplicates Status are not Allowed.');
                }

                this.getSetupData();

            }).catch(err => {

                this.toastHandler('error', 'Unable to save student/staff status.');

            });
        },

        updateStatus(payload) {

            axios.post('/schsetups/UpdateStatus', payload).then(response => {

                
                if (!response.data.isDefaultDuplicate && !response.data.isDuplicate) {

                    this.toastHandler('', 'Student/Staff status updated.');

                }
                else if (response.data.isDefaultDuplicate && response.data.isDuplicate) {

                    this.toastHandler('warning', 'Only one status can be set to default at a time.');

                }
                else if (response.data.isDefaultDuplicate) {

                    this.toastHandler('warning', 'Only one status can be set to default at a time.');

                }
                else if (response.data.isDuplicate) {

                    this.toastHandler('warning', 'Duplicates Status are not Allowed.');
                }

                this.getSetupData();

            }).catch(err => {

                this.toastHandler('error', 'Unable to update student/staff status.');

            });
        },

        delStatus(id) {

            axios.get('/schsetups/DelStatus?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Student/Staff status deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete student/staff status.');

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