Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        selectedClassId: '',
        schClasses: [],
        studentList: [],
        academicYearList: [],
        studentStatusList: [],
        religionList: [],
        nationalityList: [],
        relationshipList: [],
        studentIdentifier: '',
        selectedStudentId: '',
        studentId: null,
        tenantId: null,
        parentId: null,
        firstName: '',
        dateOfBirth: '',
        enrollmentDate: '',
        religionId: '',
        academicYearId: '',
        homeAddress: '',
        studentStatusId: '',
        lastName: '',
        middleName: '',
        nationalityId: '',
        gender: 'Male',
        enrollmentType: 'Day',
        classId: '',
        cityOrLocation: '',
        parentList: [],
        existingParent: false,
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
        studentInfo: {},
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        isSystemNumberCommitted: false,
        currentSystemNumber: {},
        studentListControllInitialized: false


    },
    validations() {

        return {

            studentIdentifier: {
                required
            },
            firstName: {
                required
            },
            lastName: {
                required, 
            },
            studentStatusId: {
                required,
            },
            classId: {
                required,
            },
            cityOrLocation: {
                required,
            },
            

        }


    },
    methods: {

        saveStudent() {

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

            if (!this.dateOfBirth) {

                this.dateOfBirth = new Date();
            }

            if (!this.enrollmentDate) {

                this.enrollmentDate = new Date();
            }

            if (!this.religionId) {

                this.religionId = this.emptyGuid;
            }


            if (!this.academicYearId) {

                this.academicYearId = this.emptyGuid;
            }

            if (!this.nationalityId) {

                this.nationalityId = this.emptyGuid;
            }

            if (!this.studentStatusId) {

                this.studentStatusId = this.emptyGuid;
            }

            this.payload.studentIdentifier = this.studentIdentifier;
            this.payload.firstName = this.firstName;
            this.payload.dateOfBirth = this.dateOfBirth;
            this.payload.enrollmentDate = this.enrollmentDate;
            this.payload.religionId = this.religionId;
            this.payload.academicYearId = this.academicYearId;
            this.payload.homeAddress = this.homeAddress;
            this.payload.studentStatusId = this.studentStatusId;
            this.payload.lastName = this.lastName;
            this.payload.middleName = this.middleName;
            this.payload.nationalityId = this.nationalityId;
            this.payload.gender = this.gender;
            this.payload.enrollmentType = this.enrollmentType;
            this.payload.classId = this.classId;
            this.payload.cityOrLocation = this.cityOrLocation;
             

            if (this.studentId != null) {

                this.payload.id = this.studentId;
                this.payload.tenantId = this.tenantId;

                axios.post('/Students/UpdateStudent', this.payload).then((response) => {

                    this.isLoading = false;
                    this.toastHandler('', 'Student updated.');
                    this.studentId = null;
                    this.tenantId = null;
                    this.getStudents();
                    this.isSystemNumberCommitted = true;


                }).catch((error) => {

                    this.isLoading = false;
                    this.toastHandler('error', 'Unable to update student.');

                });

            }
            else {

                axios.post('/Students/CreateStudent', this.payload).then((response) => {

                    this.isLoading = false;
                    this.studentId = null;
                    this.tenantId = null;
                    this.selectedStudentId = response.data.id;
                    this.getStudents();
                    this.saveParent();
                    this.isSystemNumberCommitted = true;
                    this.updateSystemNumber();
                    this.toastHandler('', 'Student created.');
                    

                }).catch((error) => {

                    this.isLoading = false;
                    this.toastHandler('error', 'Unable to create student.');

                });

            }

        },

        saveParent() {

            if (!this.existingParent && this.firstGuardianName) {

                this.payload = {};
                this.isLoading = true;

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

                if (this.parentId != null) {

                    this.payload.id = this.parentId;
                    this.payload.tenantId = this.tenantId;

                    axios.post('/Parents/UpdateParent', this.payload).then((response) => {

                        this.isLoading = false;
                        this.toastHandler('', 'Parent info updated.');
                        this.parentId = response.data.id;

                        this.saveStudentParentMapping();


                    }).catch((error) => {

                        this.isLoading = false;
                        this.toastHandler('error', 'Unable to update parent info.');

                    });

                }
                else {

                    axios.post('/Parents/CreateParent', this.payload).then((response) => {

                        this.isLoading = false;
                        this.toastHandler('', 'Parent info saved.');

                        this.parentId = response.data.id;

                        this.saveStudentParentMapping();


                    }).catch((error) => {

                        this.isLoading = false;
                        this.toastHandler('error', 'Unable to save parent info.');

                    });
                }
            }
            else {

                this.saveStudentParentMapping();

            }

            

        },

        saveStudentParentMapping() {

            this.payload = {};

            
            if (this.parentId && this.selectedStudentId) {

                this.payload.studentId = this.selectedStudentId;
                this.payload.parentId = this.parentId;

                axios.post('/Parents/CreateOrUpdateParentStudentMapping', this.payload).then((response) => {

                    this.parentId = null;
                    this.resetForm();

                }).catch((error) => {

                    console.log(error);
                });
            }

           
        },

        getSchClasses() {

            axios.get('/SchSetups/GetClasses').then(response => {

                this.schClasses = response.data;

                $("#classIdQuery").ejDropDownList({
                    dataSource: this.classList.sort((a, b) => a.className.localeCompare(b.className)),
                    watermarkText: "Select Class",
                    width: "100%",
                    fields: { id: "id", text: "className", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.selectedClassId = args.value;
                    }

                });

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getStudents() {

            if (this.selectedClassId) {

                axios.get('/Students/GetStudentByClass?id=' + this.selectedClassId).then(response => {

                    this.studentList = response.data;

                    response.data.forEach(v => {

                        v.studentName = v.middleName == '' || null ? `${v.lastName} ${v.firstName} - ${v.studentIdentifier}` : `${v.lastName} ${v.firstName} ${v.middleName} - ${v.studentIdentifier}`

                    });

                    $("#studentIdQuery").ejDropDownList({

                        dataSource: response.data.sort((a, b) => a.studentName.localeCompare(b.studentName)),
                        watermarkText: "Select Student",
                        width: "100%",
                        fields: { id: "id", text: "studentName", value: "id" },
                        enableFilterSearch: true,
                        change: (args) => {

                            this.selectedStudentId = args.value;
                        }

                    });

                    this.studentListControllInitialized = true;

                }).catch(err => {

                    console.log(err);

                    this.toastHandler('error', 'Unable to get students.');

                });
            }
           

        },

        generateStudentIdentifier() {

            
            axios.get('/SharedResource/GetSystemNumbers').then(response => {

                if (response.data.length) {

                    for (i = 0; i < response.data.length; i++) {

                        let itemName = response.data[i].itemName;

                        if (itemName == 'StudentID') {

                            this.currentSystemNumber = response.data[i];

                            this.studentIdentifier = this.currentSystemNumber.prefix + this.currentSystemNumber.lastNo + this.currentSystemNumber.suffix;

                            break;
                        }

                    }
                }
                else {

                    this.studentIdentifier = Math.floor(Math.random() * 99999) + 100;
                }
                

            }).catch(err => {


            });


        },

        updateSystemNumber() {


            if (this.isSystemNumberCommitted) {

                this.currentSystemNumber.lastNo += 1;

                axios.post('/SharedResource/UpdateSystemNumber', this.currentSystemNumber).then(response => {

                    this.isSystemNumberCommitted = false;


                }).catch(err => {


                });
            }

           
        },

        getNationalities() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.nationalityList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get nationalities.');

            });
        },

        getParents() {

            axios.get('/Parents/GetParents').then(response => {

                this.parentList = response.data;

            }).catch(err => {

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

        getAcademicYears() {

            axios.get('/SchSetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },

        getReligions() {

            axios.get('/SharedResource/GetReligions').then(response => {

                this.religionList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get religions.');

            });
        },

        getRelationships() {

            axios.get('/SharedResource/GetRelationships').then(response => {

                this.relationshipList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },

        getStudentDetail() {

            axios.get('/Students/GetStudentDetails?id=' + this.selectedStudentId).then(response => {
 
                
                this.studentIdentifier = response.data.studentIdentifier;
                this.firstName = response.data.firstName;
                this.dateOfBirth = this.formatDate(response.data.dateOfBirth);
                this.enrollmentDate = this.formatDate(response.data.enrollmentDate);
                this.religionId = response.data.religionId;
                this.academicYearId = response.data.academicYearId;
                this.homeAddress = response.data.homeAddress;
                this.lastName = response.data.lastName;
                this.middleName = response.data.middleName;
                this.nationalityId = response.data.nationalityId;
                this.classId = response.data.classId;
                this.cityOrLocation = response.data.cityOrLocation;
                this.gender = response.data.gender;
                this.studentId = response.data.id;
                this.tenantId = response.data.tenantId;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get student detail');

            });

        },

        getParentDetail() {

            axios.get('/Parents/GetParentDetails?id=' + this.selectedStudentId).then(response => {

                this.parentId = response.data.id;
                this.firstGuardianName = response.data.firstGuardianName;
                this.firstGuardianPhoneNo = response.data.firstGuardianPhoneNo;
                this.firstGuardianEmail = response.data.firstGuardianEmail;
                this.firstGuardianProfession = response.data.firstGuardianProfession;
                this.secondGuardianName = response.data.secondGuardianName;
                this.secondGuardianPhoneNo = response.data.secondGuardianPhoneNo;
                this.secondGuardianEmail = response.data.secondGuardianEmail;
                this.secondGuardianProfession = response.data.secondGuardianProfession;
                this.firstGuardianRelationshipId = response.data.firstGuardianRelationshipId;
                this.secondGuardianRelationshipId = response.data.secondGuardianRelationshipId;

                
            }).catch(err => {

                this.toastHandler('error', 'Unable to get parent detail.');

            });
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

            this.scrollTop();

        },
        scrollTop() {

            window.scroll({
                top: 0,
                left: 0,
                behavior: 'smooth'
            });

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
        }

    },

    created() {

        this.generateStudentIdentifier();
        this.getSchClasses();
        this.getReligions();
        this.getAcademicYears();
        this.getNationalities();
        this.getStudentStatus();
        this.getParents();
        this.getRelationships();
    },
});