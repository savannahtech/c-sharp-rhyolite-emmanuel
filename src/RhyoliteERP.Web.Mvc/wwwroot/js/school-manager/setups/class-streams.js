
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

            axios.get('/schsetups/GetClassStreams').then(response => {

                 
                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get class streams.');

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
                    { field: "name", headerText: "Class Stream", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveClassStream(args.data);
                },
                endEdit: (args) => {

                    this.updateClassStream(args.data);
                },
                endDelete: (args) => {

                    this.delClassStream(args.data.id);
                },
            });
        },

        saveClassStream(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateClassStream', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Class stream saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save class stream.');

            });
        },

        updateClassStream(payload) {

            axios.post('/schsetups/UpdateClassStream', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Class stream updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update class stream.');

            });
        },

        delClassStream(id) {

            axios.get('/schsetups/DelClassStream?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Class stream deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to class-streams.');

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