Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        relationshipList: [],
        parentId: null,
        selectedParentId: '',
        tenantId: null,
        parentList: [],
        firstGuardianRelationshipId: '00000000-0000-0000-0000-000000000000',
        secondGuardianRelationshipId: '00000000-0000-0000-0000-000000000000',
        firstGuardianName: '',
        firstGuardianPhoneNo: '',
        firstGuardianEmail: '',
        firstGuardianProfession: '',
        secondGuardianName: '',
        secondGuardianPhoneNo: '',
        secondGuardianEmail: '',
        secondGuardianProfession: '',
        emptyGuid: '00000000-0000-0000-0000-000000000000'

    },
    validations() {

        return {

            firstGuardianName: {
                required
            },
            firstGuardianRelationshipId: {
                required
            },

        }


    },
    methods: {

     
        saveParent() {

            this.$v.$touch();

            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }

                }

                return;
            }


            this.payload = {};
            this.isLoading = true;
            if (this.secondGuardianRelationshipId == '') {

                this.secondGuardianRelationshipId = this.emptyGuid;
            }

            this.payload.firstGuardianName = this.firstGuardianName;
            this.payload.firstGuardianPhoneNo = this.firstGuardianPhoneNo;
            this.payload.firstGuardianEmail = this.firstGuardianEmail;
            this.payload.firstGuardianProfession = this.firstGuardianProfession;
            this.payload.secondGuardianName = this.secondGuardianName;
            this.payload.secondGuardianPhoneNo = this.secondGuardianPhoneNo;
            this.payload.secondGuardianEmail = this.secondGuardianEmail;
            this.payload.secondGuardianProfession = this.secondGuardianProfession;
            this.payload.firstGuardianRelationshipId = this.firstGuardianRelationshipId;
            this.payload.secondGuardianRelationshipId = this.secondGuardianRelationshipId;

            if (this.parentId) {

                this.payload.id = this.parentId;
                this.payload.tenantId = this.tenantId;


                axios.post('/Parents/UpdateParent', this.payload).then((response) => {

                    this.isLoading = false;

                    this.toastHandler('', 'Parent info saved.');

                    this.parentId = null;

                }).catch((error) => {

                    this.isLoading = false;
                    this.toastHandler('error', 'Unable to update parent info.');

                });
            }
            else {

                axios.post('/Parents/CreateParent', this.payload).then((response) => {

                    this.isLoading = false;
                    this.toastHandler('', 'Parent info saved.');
                    this.parentId = null;

                }).catch((error) => {

                    this.isLoading = false;
                    this.toastHandler('error', 'Unable to save parent info.');

                });

            }

        },

        saveStudentParentMapping() {

            this.payload = {};

            this.payload.studentId = this.selectedStudentId;
            this.payload.parentId = this.parentId;

            axios.post('/Parents/CreateOrUpdateParentStudentMapping', this.payload).then((response) => {

                this.parentId = null;
                this.resetForm();

            }).catch((error) => {

                console.log(error);
            });
        },

        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getStudents() {

            axios.get('/Students/GetStudentByClass?id=' + this.selectedClassId).then(response => {

                this.studentList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get students.');

            });

        },

        setCurrentParentInfo() {

            let parentDetails = this.getParentDetails(this.selectedParentId);

            console.log(parentDetails);

            if (parentDetails) {

                this.parentId = parentDetails.id;
                this.tenantId = parentDetails.tenantId;
                this.firstGuardianName = parentDetails.firstGuardianName;
                this.firstGuardianPhoneNo = parentDetails.firstGuardianPhoneNo;
                this.firstGuardianEmail = parentDetails.firstGuardianEmail;
                this.firstGuardianProfession = parentDetails.firstGuardianProfession;
                this.firstGuardianRelationshipId = parentDetails.firstGuardianRelationshipId;
                this.secondGuardianName = parentDetails.secondGuardianName;
                this.secondGuardianPhoneNo = parentDetails.secondGuardianPhoneNo;
                this.secondGuardianEmail = parentDetails.secondGuardianEmail;
                this.secondGuardianProfession = parentDetails.secondGuardianProfession;
                this.secondGuardianRelationshipId = parentDetails.secondGuardianRelationshipId;
            }

        },
         
        
        getParents() {

            axios.get('/Parents/GetParents').then(response => {

                this.parentList = response.data;

                response.data.forEach(v => {

                    v.parentName = v.secondGuardianName ? `${v.firstGuardianName}, ${v.secondGuardianName} (${v.firstGuardianPhoneNo} )` : `${v.firstGuardianName} (${v.firstGuardianPhoneNo} )`

                });

                $("#parentIdQuery").ejDropDownList({
                    dataSource: response.data.sort((a, b) => a.firstGuardianName.localeCompare(b.firstGuardianName)),
                    watermarkText: "Select Parent",
                    width: "100%",
                    fields: { id: "id", text: "parentName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.selectedParentId = args.value;
                    }

                });

            }).catch(err => {

                console.log(err);
                this.toastHandler('error', 'Unable to get parents.');

            });
        },

        getStudentStatus() {

            axios.get('/SchSetups/GetStatus').then(response => {

                this.studentStatusList = response.data;
                if (this.studentStatusList.length) {

                    this.studentStatusId = this.studentStatusList[0].id;

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to get student status.');

            });
        },
        
        getRelationships() {

            axios.get('/SharedResource/GetRelationships').then(response => {

                this.relationshipList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },
         
        getParentDetails(id) {

            return this.parentList.find(a => a.id === id);
        },

        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

        },

        resetForm() {

            this.studentIdentifier = null;
            this.firstName = null;
           
            this.religionId = null;
            this.academicYearId = null;
            this.homeAddress = null;
            this.lastName = null;
            this.middleName = null;
            this.nationalityId = null;
          
            this.classId = null;
            this.cityOrLocation = null;

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

        selectedClassId(val) {

            this.getStudents();
        },
        selectedStudentId(val) {

            this.getStudentDetail();
            this.getParentDetail();
        },
        selectedParentId(val) {

            this.setCurrentParentInfo();
        }

    },

    created() {
         
        this.getRelationships();
        this.getParents();
    },
});