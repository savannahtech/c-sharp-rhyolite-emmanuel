
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        
        levelList: [],
        weightCount: 0,
        selectedLevelId: '',
    },

    methods: {


        getLevels() {

            axios.get('/schsetups/GetLevels').then(response => {

                this.levelList = response.data;

                if (this.levelList.length) {

                    this.selectedLevelId = response.data[0].id;
                    this.getSetupData();

                   

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to get levels.');

            });
        },

        getSetupData() {

            this.weightCount = 0;

            axios.get('/schsetups/GetResultTypes?id=' + this.selectedLevelId).then(response => {

                this.renderTable(response.data);

                response.data.forEach((v, index) => {
                    this.weightCount += v.percentage;

                })

            }).catch(err => {

                this.toastHandler('error', 'Unable to get result types.');

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
                    { field: "levelId", headerText: "LevelId", width: 75, visible: false, },
                    { field: "name", headerText: "Result Type", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "percentage", editType: "numericedit", headerText: "Percentage Weighting(%)", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveResultType(args.data);
                },
                endEdit: (args) => {

                    this.updateResultType(args.data);
                },
                endDelete: (args) => {

                    this.delResultType(args.data.id);
                },
            });

        },

        saveResultType(payload) {

            delete payload.id;
            delete payload.tenantId;

            payload.levelId = this.selectedLevelId;

            if (this.weightCount + payload.percentage > 100) {
                
                this.toastHandler('warning', 'Percentage Weighting Must Sum up to 100%.');

                this.getSetupData();
                return;

            }

            axios.post('/schsetups/CreateResultType', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Result type saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save result type.');

            });
        },

        updateResultType(payload) {

            axios.post('/schsetups/UpdateResultType', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Result type updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update result type.');

            });
        },

        delResultType(id) {

            axios.get('/schsetups/DelResultType?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Result type deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete result type.');

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

        this.getLevels();
    },
});