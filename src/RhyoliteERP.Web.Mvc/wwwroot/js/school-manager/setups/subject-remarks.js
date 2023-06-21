
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/schsetups/GetSubjectRemarks').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get subject remarks.');

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
                    { field: "minimumMarks", headerText: "Minimum Mark", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "maximumMarks", headerText: "Maximum Mark", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "remarks", headerText: "Remarks", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveSubjectRemark(args.data);
                },
                endEdit: (args) => {

                    this.updateSubjectRemark(args.data);
                },
                endDelete: (args) => {

                    this.delSubjectRemark(args.data.id);
                },
            });

        },

        saveSubjectRemark(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateSubjectRemark', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Subject Remarks saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save subject remarks.');

            });
        },

        updateSubjectRemark(payload) {

            axios.post('/schsetups/UpdateSubjectRemark', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Subject Remarks updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update subject remarks.');

            });
        },

        delSubjectRemark(id) {

            axios.get('/schsetups/DelSubjectRemark?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Subject Remarks deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete subject remarks.');

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