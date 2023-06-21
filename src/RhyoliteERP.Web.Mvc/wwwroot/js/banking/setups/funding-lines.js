
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getSetupData() {

            axios.get('/BankingSetups/GetFundingLines').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get funding lines');

            });
        },


        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData.fundingLines,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Account Type", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "beginDate", headerText: "Begin Date", format: "{0:dd-MMM-yyyy}", validationRules: { date: true }, editType: ej.Grid.EditingType.DatePicker, width: 75, textAlign: ej.TextAlign.Left },
                    { field: "endDate", headerText: "End Date", width: 80, format: "{0:dd-MMM-yyyy}", validationRules: { date: true }, editType: ej.Grid.EditingType.DatePicker, textAlign: ej.TextAlign.Left },
                    { field: "amount", headerText: "Amount", editType: "numericedit", editParams: { decimalPlaces: 4 }, validationRules: { required: true, }, width: 55 },
                    { field: "currencyId", headerText: "Currency", editType: ej.Grid.EditingType.Dropdown, foreignKeyField: "id", foreignKeyValue: "currencyName", dataSource: responseData.currencies, textAlign: ej.TextAlign.Left, width: 55 },
                    { field: "purpose", headerText: "Purpose", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveFundingLine(args.data);
                },
                endEdit: (args) => {

                    this.updateFundingLine(args.data);
                },
                endDelete: (args) => {

                    this.delFundingLine(args.data.id);
                },
            });

        },

        saveFundingLine(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/BankingSetups/CreateFundingLine', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Funding Line saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save funding line.');

            });
        },

        updateFundingLine(payload) {

            axios.post('/BankingSetups/UpdateFundingLine', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Funding line updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update funding line.');

            });
        },

        delFundingLine(id) {

            axios.get('/BankingSetups/DelFundingLine?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Funding line deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete funding line.');

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