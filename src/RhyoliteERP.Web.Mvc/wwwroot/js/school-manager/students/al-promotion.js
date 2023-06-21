Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        promotedFromClassId: '',
        schClasses: [],
        studentsPromotionData: []
       

    },
    validations() {

        return {

            studentIdentifier: {
                required
            },
            

        }


    },
    methods: {

        getStudentsByClass() {

            axios.get('/Students/GetStudentByClass?id=' + this.promotedFromClassId).then(response => {

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
        promoteStudents() {


            if (this.promotedFromClassId == '') {

                this.toastHandler('warning', 'Select a class');

                return;
            }

            this.isLoading = true;
            this.getStudents();

            this.studentsPromotionData.forEach( (v) => {

                v.primaryId = v.id;
               
            });

            if (this.studentsPromotionData.length != 0) {

                this.payload.students = this.studentsPromotionData;

                axios.post('/Students/PromoteAlumni', this.payload).then(response => {

                    this.toastHandler('', 'Alumni Promotion Successful..!');
                    this.isLoading = false;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to promote alumni.\nAlumni already promoted.');
                    this.isLoading = false;

                });

            }
            else {
                this.isLoading = false;

                this.toastHandler('warning', 'No Students Have Been Selected For Promotion..!');

            }
        },
        getStudents() {

            let obj = $("#dataTable").ejGrid("instance"), rec = []; studsProm = [];
            let check = obj.checkSelectedRowsIndexes;  // collection which holds the page index and the selected record index
            if (check.length) {
                for (var pageInx = 0; pageInx < check.length; pageInx++) {
                    if (!ej.isNullOrUndefined(check[pageInx]))
                        rec = this.getRecords(pageInx, check[pageInx], obj, rec);
                }
            }

            this.studentsPromotionData = rec;


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

        promotedFromClassId(val) {

            this.getStudentsByClass();
        },
         

    },

    created() {

        this.getSchClasses();
        
    },
});