
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        academicYearList: [],
        staffList: [],
        schClasses: [],
        termList: [],
        selectedAcademicYearId: '',
        selectedTermId: '',
        selectedClassId: '',
        selectedStaffId: '',
        selectedSubjectList: [],

    },

    methods: {

        assignSubjects() {

            this.getSelectedSubjects();

            if (this.selectedSubjectList.length) {

                this.payload = {};

                this.payload.AcademicYearId = this.selectedAcademicYearId;
                this.payload.TermId = this.selectedTermId;


                axios.post('/Staff/CreateSubject', payload).then(response => {

                    this.getSetupData();
                    this.toastHandler('', 'Subject(s) successfully assigned.!');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to assign subjects.');

                });

            }
            else {

                this.toastHandler('warning', 'No Subjects have been selected for Assignment');

            }

        },

        getStaff() {

            axios.get('/Staff/GetStaff').then(response => {

                response.data.forEach((v) => {

                    v.dateEmployed = this.formatDate(v.dateEmployed);
                })

                this.staffList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff.');

            });
        },

        getAcademicYears() {

            axios.get('/SchSetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },

        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getSubjects() {

            axios.get('/schsetups/GetSubjects').then(response => {

                this.renderSubjectsTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get subjects.');

            });
        },

        renderSubjectsTable(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                toolbarSettings: { showToolbar: true, },
                columns: [

                    { type: "checkbox", width: 10 },
                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Subject", width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],

            });

        },

        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

        },
        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
        },

        getSelectedSubjects() {

            var obj = $("#dataTable").ejGrid("instance"), rec = []; selectedSubjectList = [];
            var check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
            if (check.length) {
                for (var pageInx = 0; pageInx < check.length; pageInx++) {
                    if (!ej.isNullOrUndefined(check[pageInx]))
                        rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                }
            }

            this.selectedSubjectList = rec;
        },

        getRecords(pageInx, inx, proxy, rec) {

            if (inx.length) {
                for (var i = 0; i < inx.length; i++) {
                    var pageSize = proxy.model.pageSettings.pageSize;  //gets the page size of grid
                    var data = proxy.model.dataSource[pageInx * pageSize + inx[i]];
                    rec.push(data);
                    //pushing all the selected Records
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
        },
        
    },
    watch: {

        selectedAcademicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }

        },
    },
    created() {

        this.getStaff();
        this.getAcademicYears();
        this.getSchClasses();
        this.getSubjects();
        
        
    },
});