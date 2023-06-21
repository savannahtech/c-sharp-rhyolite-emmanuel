
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        academicYearList: [],
        termList: [],
        schClasses: [],
        billTypeList: [],
        feesDescriptionList: [],
        selectedBillTypeId: '',
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        selectedFeeId: '',
        totalBillAmount: 0,
        feeAmount: 0,
        billDetails: []

         
    },

    methods: {


        getAcademicYears() {

            axios.get('/schsetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;


            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },

        getSchClasses() {

            axios.get('/schsetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getBillTypes() {

            axios.get('/schsetups/GetBillTypes').then(response => {

                this.billTypeList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get bill types.');

            });
        },

        getFeesDescriptions() {

            axios.get('/schsetups/GetFeesDescriptions?id=' + this.selectedBillTypeId).then(response => {

                this.feesDescriptionList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get fees descriptions.');

            });
        },

        getSetupData() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedBillTypeId) {

                axios.get('/schsetups/GetBillSetups?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&billTypeId=' + this.selectedBillTypeId).then(response => {

                    this.billDetails = [];

                    if (response.data && response.data.details)
                    {
                        response.data.details.forEach((detail) => {

                            detail.headerId = response.data.id;
                            this.billDetails.push(detail);

                        })
                    }

                    this.renderTable(this.billDetails);


                }).catch(err => {


                });

            }

        },

        renderTable(responseData) {

            
            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                sortSettings: { sortedColumns: [{ field: "feeAmount", direction: "ascending" }] },
                editSettings: { allowEditing: true, allowAdding: false, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                summaryRows: [
                    { title: "Bill Amount", summaryColumns: [{ summaryType: ej.Grid.SummaryType.Sum, displayColumn: "feeAmount", dataMember: "feeAmount", format: "{0:n2}" }] },

                ],
                columns: [

                    { field: "id", isPrimaryKey: true, visible: false, headerText: "id", width: 75, },
                    { field: "headerId", isPrimaryKey: true, visible: false, headerText: "id", width: 75, },
                    { field: "billTypeId", visible: false, headerText: "BillTypeId", width: 75, },
                    { field: "feeId", visible: false, headerText: "FeeId", width: 75, },
                    { field: "feeName", headerText: "Bill Fee", width: 75, allowEditing: false }, //
                    { field: "feeAmount", headerText: "Amount", format: "{0:n2}", textAlign: ej.TextAlign.Right, width: 75, },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },
                ],
                
                endEdit: (args) => {

                    this.updateBillSetupDetail(args.data);
                },
                endDelete: (args) => {

                    this.delBillSetupDetail(args.data);
                },
            });

        },

        saveBillSetup() {

            this.payload = {};

            this.isLoading = true;

            this.payload.AcademicYearId = this.selectedAcademicYearId;
            this.payload.TermId = this.selectedTermId;
            this.payload.ClassId = this.selectedClassId;
            this.payload.BillTypeId = this.selectedBillTypeId;
            this.payload.TotalBillAmount = this.totalBillAmount;

            this.payload.Details = [{ feeId: this.selectedFeeId, feeAmount: this.feeAmount, billTypeId: this.selectedBillTypeId, id: this.uuid()  }];

            axios.post('/schsetups/CreateBillSetup', this.payload).then(response => {

                this.getSetupData();
                this.feeAmount = 0;
                this.isLoading = false;


            }).catch(err => {

                this.isLoading = false;

                this.toastHandler('error', 'Unable to save bill setup.');

            });

        },

        updateBillSetupDetail(payload) {

            this.payload = {};

            this.payload.AcademicYearId = this.selectedAcademicYearId;
            this.payload.TermId = this.selectedTermId;
            this.payload.ClassId = this.selectedClassId;
            this.payload.BillTypeId = this.selectedBillTypeId;
            this.payload.TotalBillAmount = this.totalBillAmount;

            this.payload.Details = [{ feeId: payload.feeId, feeAmount: payload.feeAmount, billTypeId: payload.billTypeId, id: payload.id }];
                
            axios.post('/schsetups/CreateBillSetup', this.payload).then(response => {

                this.getSetupData();
                this.feeAmount = 0;

            }).catch(err => {


                this.toastHandler('error', 'Unable to save bill setup.');

            });

        },

        delBillSetupDetail(payload) {

            axios.get('/schsetups/DelBillSetupDetail?id=' + payload.id + '&headerId=' + payload.headerId).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Result type deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete result type.');

            });
        },
        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
        },
        uuid() {

            return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
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
    computed: {

        classList() {

            this.schClasses.forEach((v) => {

                if (v.streamId != '00000000-0000-0000-0000-000000000000') {
                    v.className = v.className + '-' + v.streamName;
                } 
 
            });

            return this.schClasses;
        }
    },
    watch: {

        selectedBillTypeId(val) {
            
            if (val) {
                this.getFeesDescriptions();
                this.getSetupData();
            }
        },
        selectedAcademicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }
            
        },

    },
    created() {

        this.getAcademicYears();
        this.getSchClasses();
        this.getBillTypes();
    },
});