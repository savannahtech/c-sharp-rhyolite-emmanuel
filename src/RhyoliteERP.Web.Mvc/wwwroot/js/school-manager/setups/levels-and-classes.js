
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        levelList: [],
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

            
            axios.get('/schsetups/GetClassesByLevel?id=' + this.selectedLevelId).then(response => {

                this.renderTable(response.data.classes, response.data.streams);


            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        renderTable(classes, streams) {

            $("#dataTable").ejGrid({

                dataSource: classes,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "levelId", headerText: "LevelId", width: 75, visible: false },
                    { field: "className", headerText: "Class", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "streamId", headerText: "Stream", editType: ej.Grid.EditingType.Dropdown, foreignKeyField: "id", foreignKeyValue: "name", dataSource: streams, textAlign: ej.TextAlign.Left, width: 55 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveClass(args.data);
                },
                endEdit: (args) => {

                    this.updateClass(args.data);
                },
                endDelete: (args) => {

                    this.delClass(args.data.id);
                },
            });

        },

        saveClass(payload) {

            delete payload.id;
            delete payload.tenantId;

            payload.levelId = this.selectedLevelId;

            if (payload.streamId == null) {
                payload.streamId = "00000000-0000-0000-0000-000000000000";
            }

            axios.post('/schsetups/CreateClass', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Class saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save class.');

            });
        },

        updateClass(payload) {

            if (payload.streamId == null) {
                payload.streamId = "00000000-0000-0000-0000-000000000000";
            }

            axios.post('/schsetups/UpdateClass', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Class updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update class.');

            });
        },

        delClass(id) {

            axios.get('/schsetups/DelClass?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Class deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete class.');

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