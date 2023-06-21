Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        attendanceDate: moment().format("YYYY-MM-DD"),
        classId: '',
        noPresent: 0,
        schClasses: [],
        studentsAttendanceData: []
       

    },
  
    methods: {

        getStudentsByClass() {

            axios.get('/Students/GetStudentByClass?id=' + this.classId).then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get students.');

            });
        },

       renderTable(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                isResponsive: true,
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Search, ej.Grid.ToolBarItems.PrintGrid] },
                columns: [

                    { type: "checkbox", width: 10 },
                    { field: "id", headerText: "Id", visible: false, width: 15 },
                    { field: "dateOfBirth", headerText: "DateOfBirth", visible: false, width: 15 },
                    { field: "nationalityId", headerText: "NationalityId", visible: false, width: 15 },
                    { field: "enrollmentDate", headerText: "EnrollmentDate", visible: false, width: 15 },
                    { field: "homeAddress", headerText: "HomeAddress", visible: false, width: 15 },
                    { field: "gender", headerText: "Gender", visible: false, width: 15 },
                    { field: "religionId", headerText: "ReligionId", visible: false, width: 15 },
                    { field: "classId", headerText: "ClassId", visible: false, width: 15 },
                    { field: "academicYearId", headerText: "AcademicYearId", visible: false, width: 15 },
                    { field: "studImage", headerText: "StudImage", visible: false, width: 15 },
                    { field: "parentId", headerText: "ParentId", visible: false, width: 15 },
                    { field: "studentStatusId", headerText: "StudentStatusId", visible: false, width: 15 },
                    { field: "studentIdentifier", headerText: "Student ID", width: 35 },
                    { field: "lastName", headerText: "Last Name", width: 60 },
                    { field: "firstName", headerText: "First Name", width: 60 },
                    { field: "middleName", headerText: "Other Name", width: 60 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },


                ]
            });

       },

        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },
        saveAttendance() {

            this.isLoading = true;
            this.payload = {};
            
            this.studentsAttendanceData.forEach((v) => {

                v.studentId = v.id;
                v.studentName = v.middleName ? v.firstName + ' ' + v.lastName + ' ' + v.middleName : v.firstName + ' ' + v.lastName;

            });

            if (this.studentsAttendanceData.length != 0) {

                this.payload.details = this.studentsAttendanceData;
                this.payload.classId = this.classId;
                this.payload.attendanceDate = this.attendanceDate;
                this.payload.noPresent = this.studentsAttendanceData.length;


                axios.post('/Students/CreateStudentAttendance', this.payload).then(response => {

                    this.toastHandler('', 'Attendance saved successfully.');
                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to mark attendance.');
                    this.isLoading = false;

                });

            }
            else {
                this.isLoading = false;

                this.toastHandler('warning', 'No Student was marked present');

            }
        },
        getStudentsPresent() {

            let obj = $("#dataTable").ejGrid("instance"), rec = []; studsProm = [];
            let check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
            if (check.length) {
                for (var pageInx = 0; pageInx < check.length; pageInx++) {
                    if (!ej.isNullOrUndefined(check[pageInx]))
                        rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                }
            }

            this.studentsAttendanceData = rec;
            this.noPresent = this.studentsAttendanceData.length;


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
        }
    },
    watch: {

        classId(val) {

            this.getStudentsByClass();
        },
         

    },

    created() {

        this.getSchClasses();
        
    },
});