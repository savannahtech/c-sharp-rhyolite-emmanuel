
Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

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
        studentList: [],
        selectedBillTypeId: '',
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        billNo: '',
        billDate: moment().format("YYYY-MM-DD"),
        billDetails: [],
        studentBills: [],
        totalBillAmount: 0,
        currentBillSetupId: '',
    },
    validations() {

        return {

            selectedAcademicYearId: {
                required
            },
            selectedTermId: {
                required
            },
            selectedClassId: {
                required,
            },
            selectedBillTypeId: {
                required,
            },
            billNo: {
                required,
            },
            billDate: {
                required,
            },


        }


    },
    methods: {

        generateBills() {

            this.isLoading = true;

            this.getStudBillData();

            if (!this.billDetails.length) {

                this.toastHandler('warning', 'There are no bill items for bill processing.');
                this.isLoading = false;

                return;
            }


            if (!this.studentBills.length) {

                this.toastHandler('warning', 'No student(s) selected for bill processing..!');
                this.isLoading = false;

                return;

            }


            this.payload = {};

           
            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedBillTypeId) {


                this.billDetails.forEach((v) => {
                    
                    v.billTypeId = this.selectedBillTypeId;
                
                });

                this.studentBills.forEach( (v) => {

                    v.academicYearId = this.selectedAcademicYearId;
                    v.classId = this.selectedClassId;
                    v.termId = this.selectedTermId;
                    v.billNo = this.billNo;
                    v.billDate = this.billDate;
                    v.billAmount = this.totalBillAmount;
                    v.billBalance = this.totalBillAmount;
                    v.billTypeId = this.selectedBillTypeId;
                    v.billSetupId = this.currentBillSetupId;
                    v.studentId = v.id;
                    v.billStatus = 301; // 201 => Paid ; 301 => Unpaid ; 401 => Part Payment;
                    v.Details = this.billDetails;
                });

                axios.post('/BillsAndPayments/CreateBills', this.studentBills).then(response => {

                    this.billDetails = [];
                    this.studentList = [];
                    this.studentBills = [];
                    this.totalBillAmount = 0;

                    this.toastHandler('', 'Bills processed successfully.');
                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to generate bills.');
                    this.isLoading = false;
                    this.totalBillAmount = 0;


                });


            }

        },
        generateBillNo() {

            //if system numbers is empty
            this.billNo = Math.floor(Math.random() * 99999) + 100;

        },

        getBill() {


            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedBillTypeId) {

                axios.get('/schsetups/GetBillSetups?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&billTypeId=' + this.selectedBillTypeId).then(response => {

                    this.billDetails = [];

                    if (response.data) {

                        this.currentBillSetupId = response.data.id;
                        response.data.details.forEach((detail) => {

                            detail.headerId = this.currentBillSetupId;
                            this.totalBillAmount += detail.feeAmount;
                            this.billDetails.push(detail);

                        })


                        console.log(this.billDetails);

                        this.renderBillTable(this.billDetails);
                    }
                   


                }).catch(err => {

                    this.toastHandler('error', 'Unable to get bill setup.');

                });

            }

        },

        renderBillTable(responseData) {


            $("#billsTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                sortSettings: { sortedColumns: [{ field: "feeAmount", direction: "ascending" }] },
                editSettings: { allowEditing: true, allowAdding: false, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
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

                
                 
            });

        },
       
        getStudents() {

            axios.get('/Students/GetStudentByClass?id=' + this.selectedClassId).then(response => {

                this.studentList = response.data;
                this.renderStudentsTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get students.');

            });

        },

        renderStudentsTable(responseData) {


            $("#studentsTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                isResponsive: true,
                columns: [

                    { type: "checkbox", width: 10 },
                    { field: "id", isPrimaryKey: true, visible: false, headerText: "id", width: 75, },
                    { field: "studentIdentifier", headerText: "Student ID", width: 65, },
                    { field: "lastName", headerText: "Last Name", width: 75, },
                    { field: "firstName", headerText: "First Name", width: 75, },
                    { field: "middleName", headerText: "Middle Name", width: 75, }
                ]
            });

        },

        getBillNo() {

            if (this.selectedAcademicYearId && this.selectedTermId && this.selectedClassId && this.selectedBillTypeId) {

                axios.get('/BillsAndPayments/GetBillSetup?academicYearId=' + this.selectedAcademicYearId + '&termId=' + this.selectedTermId + '&classId=' + this.selectedClassId + '&billTypeId=' + this.selectedBillTypeId).then(response => {

                    if (response.data) {

                    }

                }).catch(err => {

                    //call bot logger here...
                });

             }
            
        },

        getAcademicYears() {

            axios.get('/schsetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;


            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },
        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
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

        getStudBillData() {

            var obj = $("#studentsTable").ejGrid("instance"), rec = []; this.studentBills = [];
            var check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
            if (check.length) {
                for (var pageInx = 0; pageInx < check.length; pageInx++) {
                    if (!ej.isNullOrUndefined(check[pageInx]))
                        rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                }
            }

            this.studentBills = rec;
        },
        getRecords(pageInx, inx, proxy, rec) {

            if (inx.length) {
                for (var i = 0; i < inx.length; i++) {
                    var pageSize = proxy.model.pageSettings.pageSize;  //gets the page size of grid
                    var data = proxy.model.dataSource[pageInx * pageSize + inx[i]];
                    rec.push(data);     //pushing all the selected Records
                }
            }
            return rec;


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

        selectedAcademicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }

        },
        selectedClassId(val) {

            if (val) {

                this.getStudents();
            }

        },

    },
    created() {

        this.getAcademicYears();
        this.getSchClasses();
        this.getBillTypes();
        this.generateBillNo();

    },
});