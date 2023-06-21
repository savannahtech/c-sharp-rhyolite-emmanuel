 

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        academicYearList: [],
        termList: []
        
    },

    methods: {
       
        getSetupData() {

            axios.get('/schsetups/GetAcaYrs').then(response => {


                this.academicYearList = response.data;

                this.academicYearList.forEach((acaYr) => {

                    acaYr.academicYearId = acaYr.id;
                    acaYr.beginDate = moment(acaYr.beginDate).format("DD-MMM-YYYY");
                    acaYr.endDate = moment(acaYr.endDate).format("DD-MMM-YYYY");

                    acaYr.terms.forEach((term) => {

                        term.academicYearId = acaYr.id;
                        term.startDate = moment(term.startDate).format("DD-MMM-YYYY");
                        term.endDate = moment(term.endDate).format("DD-MMM-YYYY");
                        this.termList.push(term);

                    })

                })


                this.renderTable(response.data, this.termList);

            }).catch(err => {

                this.toastHandler('error','Unable to get academic years.');
                 
            });
        },
       
        renderTable(responseData)
        {

           $("#dataTable").ejGrid({
                    dataSource: responseData,
                    allowPaging: true,
                    allowSorting: true,
                    isResponsive: true,
                    editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                    toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                    childGrid: {
                        dataSource: this.termList,
                        queryString: "academicYearId",
                        allowPaging: true,
                        allowSorting: true,
                        isResponsive: true,
                        pageSettings: { pageSize: 10 },
                        editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                        toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                        columns: [

                            { field: "id", headerText: 'Id', visible: false, isPrimaryKey: true, width: 80 },
                            { field: "academicYearId", headerText: 'AcademicYearId', visible: false, width: 80 },
                            { field: "name", headerText: 'Term', textAlign: ej.TextAlign.Left, width: 60 },
                            { field: "precedenceNo", headerText: "Order", defaultValue: 1, editType: ej.Grid.EditingType.Numeric, editParams: { decimalPlaces: 0 }, format: "{0:n0}", validationRules: { required: true, }, width: 40 },
                            { field: "noOfDays", headerText: "No Of Days", defaultValue: 1, editType: ej.Grid.EditingType.Numeric, editParams: { decimalPlaces: 0 }, format: "{0:n0}", validationRules: { required: true, range: [1, 550] }, width: 55 },
                            { field: "startDate", headerText: "Start Date", width: 80, format: "{0:dd-MMM-yyyy}", validationRules: { date: true }, editType: ej.Grid.EditingType.DatePicker, textAlign: ej.TextAlign.Left },
                            { field: "endDate", headerText: "End Date", width: 80, format: "{0:dd-MMM-yyyy}", validationRules: { date: true }, editType: ej.Grid.EditingType.DatePicker, textAlign: ej.TextAlign.Left },
                            { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                        ],
                        endAdd: (args) => {

                           this.saveTerm(args.data);
                        },
                        endEdit: (args) => {

                            this.updateTerm(args.data);
                        },
                        endDelete: (args) => {

                            this.delTerm(args.data.id);
                        },

                    },
                    columns: [
                        { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                        { field: "name", headerText: "Academic Year", validationRules: { required: true, minlength: 1 }, width: 80 },
                        { field: "precedenceNo", headerText: "Order", format: "{0:n0}", validationRules: { required: true, minlength: 1 }, width: 40 },
                        { field: "beginDate", headerText: "Begin Date", format: "{0:dd-MMM-yyyy}", validationRules: { date: true }, editType: ej.Grid.EditingType.DatePicker, width: 75, textAlign: ej.TextAlign.Left },
                        { field: "endDate", headerText: "End Date", width: 80, format: "{0:dd-MMM-yyyy}", validationRules: { date: true }, editType: ej.Grid.EditingType.DatePicker, textAlign: ej.TextAlign.Left },
                        { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                    ],
                    endAdd: (args) => {

                        this.saveAcademicYear(args.data);
                    },
                    endEdit: (args) => {

                        this.updateAcademicYear(args.data);
                    },
                    endDelete: (args) => {

                        this.deleteAcademicYear(args.data.id);
                    },
                });

        },

        saveAcademicYear(payload) {

            delete payload.id;
            delete payload.tenantId;

            payload.beginDate = payload.beginDate.toLocaleDateString("en-US");
            payload.endDate = payload.endDate.toLocaleDateString("en-US");


            axios.post('/schsetups/CreateAcaYear', payload ).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Academic year saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save academic year.');

            });
        },

        updateAcademicYear(payload) {

            payload.beginDate = payload.beginDate.toLocaleDateString("en-US");
            payload.endDate = payload.endDate.toLocaleDateString("en-US");

            axios.post('/schsetups/UpdateAcaYr', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Academic year updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update academic year.');

            });
        },

        deleteAcademicYear(id) {

            axios.get('/schsetups/DelAcaYr?id='+id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Academic year deleted.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to delete academic year.');

            });
        },

        saveTerm(payload) {

            payload.id = this.uuid();
            payload.startDate = payload.startDate.toLocaleDateString("en-US");
            payload.endDate = payload.endDate.toLocaleDateString("en-US");

            axios.post('/schsetups/CreateTerm', payload).then(response => {

                this.toastHandler('', 'Term saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save term.');

            });
        },

        updateTerm(payload) {

            payload.startDate = payload.startDate.toLocaleDateString("en-US");
            payload.endDate = payload.endDate.toLocaleDateString("en-US");

            axios.post('/schsetups/UpdateTerm', payload).then(response => {

                this.toastHandler('', 'Term updated.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to update term.');

            });
        },

        delTerm(payload) {

            axios.post('/schsetups/DeleteTerm', payload).then(response => {

                this.toastHandler('', 'Term delete.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete term.');

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

        uuid() {

            return([1e7]+ -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
        }
    },
     
    created() {

        this.getSetupData();
    },
});