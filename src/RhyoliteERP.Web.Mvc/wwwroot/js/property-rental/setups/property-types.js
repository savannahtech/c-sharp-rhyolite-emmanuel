
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/RentalSetups/GetPropertyTypes').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get property types.');

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
                    { field: "name", headerText: "Property Type", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.savePropertyType(args.data);
                },
                endEdit: (args) => {

                    this.updatePropertyType(args.data);
                },
                endDelete: (args) => {

                    this.delPropertyType(args.data.id);
                },
            });

        },

        savePropertyType(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/RentalSetups/CreatePropertyType', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Property type saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save property type.');

            });
        },

        updatePropertyType(payload) {

            axios.post('/RentalSetups/UpdatePropertyType', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Property type updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update property type.');

            });
        },

        delPropertyType(id) {

            axios.get('/RentalSetups/DelPropertyType?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Property type deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete property type.');

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