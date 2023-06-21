Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        schClasses: [],
        academicYearList: [],
        termList: [],
        subjectList: [],
        resultTypeList: [],
        academicYearId: '',
        termId: '',
        classId: '',
        subjectId: '',
        resultTypeId: '',

    },
   
    methods: {

        getResults() {

            if (this.academicYearId && this.termId && this.classId && this.subjectId && this.resultTypeId) {

                axios.get('/Students/GetStudentResults?academicYearId=' + this.academicYearId + '&termId=' + this.termId + '&classId=' + this.classId + '&subjectId=' + this.subjectId + '&resultTypeId=' + this.resultTypeId).then(response => {


                    this.renderTable(response.data);

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get results.');

                });
            }

            
        },

       renderTable(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "academicYearId", headerText: "AcaYrId", visible: false, width: 10 },
                    { field: "classId", headerText: "ClassId", visible: false, width: 10 },
                    { field: "termId", headerText: "TermId", visible: false, width: 10 },
                    { field: "subjectId", headerText: "SubjectId", visible: false, width: 10 },
                    { field: "resultTypeId", headerText: "ResultTypeId", visible: false, width: 10 },
                    { field: "studentIdentifier", headerText: "StudentID", visible: false, width: 10 },
                    { field: "studentId", headerText: "StudentId", visible: false, width: 10 },
                    { field: "studentName", headerText: "Student Name", width: 80 },
                    { field: "marksObtained", headerText: "Marks Obtained", width: 75 },
                    { field: "totalMarks", headerText: "Total Marks", width: 55 },

                ],
                endEdit: (args) => {

                    this.editAcaResult(args.data);
                },
                endDelete: (args) => {

                    this.delAcaResult(args.data.id);
                },
            });

       },
        editAcaResult(payload) {

            axios.post('/Students/UpdateStudentResult', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Result updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update result.');

            });
        },
        delAcaResult(id) {

            axios.get('/Students/DelStudentResult?id=' + id).then(response => {

                this.getResults();

                this.toastHandler('', 'Result deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete result.');

            });
        },
        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

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

            axios.get('/SchSetups/GetSubjects').then(response => {

                this.subjectList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get subjects.');

            });
        },
        getResultTypes() {

            axios.get('/SchSetups/GetResultTypesByClass?id='+this.classId).then(response => {

                this.resultTypeList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },
        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
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

        
        academicYearId(val) {

            if (val) {

                let academicYear = this.getAcademicYearDetails(val);
                this.termList = academicYear.terms;
            }

        },

    },

    created() {

        this.getSchClasses();
        this.getAcademicYears();
        this.getSubjects();
    },
});