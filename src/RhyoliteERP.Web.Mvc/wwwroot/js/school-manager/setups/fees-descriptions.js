
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
        billTypeList: [],
        selectedBillType: '',
    },

    methods: {


        getbillTypes() {

            axios.get('/schsetups/GetBillTypes').then(response => {

                this.billTypeList = response.data;

                if (this.billTypeList.length) {

                    this.selectedBillType = response.data[0].id;
                    this.getSetupData();

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to get bill types.');

            });
        },

        getSetupData() {

            axios.get('/schsetups/GetFeesDescriptions?id=' + this.selectedBillType).then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get fees descriptions.');

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
                    { field: "billTypeId", headerText: "BillTypeId", width: 75, visible: false, },
                    { field: "description", headerText: "Fees Description", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveFeesDescription(args.data);
                },
                endEdit: (args) => {

                    this.updateFeesDescription(args.data);
                },
                endDelete: (args) => {

                    this.delFeesDescription(args.data.id);
                },
            });

        },

        saveFeesDescription(payload) {

            delete payload.id;
            delete payload.tenantId;

            payload.billTypeId = this.selectedBillType;

            axios.post('/schsetups/CreateFeesDescription', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Fees description saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save fees description.');

            });
        },

        updateFeesDescription(payload) {

            axios.post('/schsetups/UpdateFeesDescription', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Fees description updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update fees description.');

            });
        },

        delFeesDescription(id) {

            axios.get('/schsetups/DelFeesDescription?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Fees description deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete fees description.');

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

        this.getbillTypes();
    },
});