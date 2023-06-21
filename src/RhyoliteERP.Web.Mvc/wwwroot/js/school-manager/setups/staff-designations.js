
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/schsetups/GetStaffDesignations').then(response => {

                 
                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff designations.');

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
                    { field: "name", headerText: "Staff Designations", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveStaffDesignation(args.data);
                },
                endEdit: (args) => {

                    this.updateStaffDesignation(args.data);
                },
                endDelete: (args) => {

                    this.delStaffDesignation(args.data.id);
                },
            });

        },

        saveStaffDesignation(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/schsetups/CreateStaffDesignation', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Staff designation saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save staff designation.');

            });
        },

        updateStaffDesignation(payload) {

            axios.post('/schsetups/UpdateStaffDesignation', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Staff designation updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update staff designation.');

            });
        },

        delStaffDesignation(id) {

            axios.get('/schsetups/DelStaffDesignation?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Staff designation deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete staff designation.');

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