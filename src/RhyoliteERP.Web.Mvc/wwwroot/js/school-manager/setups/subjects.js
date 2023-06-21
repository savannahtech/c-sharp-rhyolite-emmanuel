
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/schsetups/GetSubjects').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get subjects.');

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
                    { field: "name", headerText: "Subject", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveSubject(args.data);
                },
                endEdit: (args) => {

                    this.updateSubject(args.data);
                },
                endDelete: (args) => {

                    this.delSubject(args.data.id);
                },
            });

        },

        saveSubject(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateSubject', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Subject saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save subject.');

            });
        },

        updateSubject(payload) {

            axios.post('/schsetups/UpdateSubject', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Subject updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update subject.');

            });
        },

        delSubject(id) {

            axios.get('/schsetups/DelSubject?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Subject deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete subject.');

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