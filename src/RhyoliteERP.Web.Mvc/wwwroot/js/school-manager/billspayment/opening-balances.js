
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
        selectedStudentId: '',
        selectedCurrencyId: '',
        schClasses: [],
        currencyList: [],
        billAmount: 0,
        paymentDate: moment().format("YYYY-MM-DD"),
        emptyGuid: '00000000-0000-0000-0000-000000000000',

    },
    
    methods: {

        createOpeningBalance() {

            this.isLoading = true;

            var payload = {};

            payload.academicYearId = this.selectedAcademicYearId;
            payload.termId = this.selectedTermId;
            payload.classId = this.selectedClassId;
            payload.studentId = this.selectedStudentId;
            
            payload.billAmount = this.billAmount;
            payload.billBalance = this.billAmount;
            payload.billTypeId = this.emptyGuid;
            payload.billSetupId = this.emptyGuid;
            payload.billDate = this.paymentDate;
            payload.billSetupInfo = null;
            payload.details = null;
            payload.billStatus = 401;
            payload.billNo = Math.floor((Math.random() * 9999999) + 10000),
            
            
            axios.post('/BillsAndPayments/CreateOpeningBalance', payload).then(response => {

                this.toastHandler('', 'Opening balance processed successfully.');
                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Unable to process opening balance.');
                this.isLoading = false;

            });

        },

        getAcademicYears() {

            axios.get('/schsetups/GetAcaYrs').then(response => {

                this.academicYearList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get academic years.');

            });
        },

        getCurrency() {

            axios.get('/SharedResource/GetCurrencys').then(response => {

                this.currencyList = response.data;
                if (this.currencyList.length) {

                    this.selectedCurrencyId = this.currencyList[0].id;
                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to get currency.');

            });
        },

        getAcademicYearDetails(id) {

            return this.academicYearList.find(a => a.id === id);
        },

        getTermDetails(id) {

            return this.termList.find(a => a.id === id);
        },
        getCurrencyDetails(id) {

            return this.currencyList.find(a => a.id === id);
        },

        getSchClasses() {

            axios.get('/schsetups/GetClasses').then(response => {

                this.schClasses = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get classes.');

            });
        },

        getStudents() {

            if (this.selectedClassId) {

                axios.get('/Students/GetStudentByClass?id=' + this.selectedClassId).then(response => {

                    this.studentList = response.data;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to get students.');

                });

            }
           

        },
         

        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

        },

        formatShortDate(dateInput) {

            return moment(dateInput).format("DD-MMM-YYYY");

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

        selectedClassId(val) {

            this.getStudents();
        },

         
    },
    created() {

        this.getAcademicYears();
        this.getSchClasses();
        this.getCurrency();
    },
});