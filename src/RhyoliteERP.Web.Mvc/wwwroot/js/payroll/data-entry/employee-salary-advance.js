Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

 

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        employeeList: [],
        employeeId: '',
        id: '',
        tenantId: '',
        loanTypeList: [],
        loanDate: '',
        amount: null,
        loanTypeId: ''


    },
    validations() {

        return {

            loanTypeId: {
                required
            },
            loanDate: {
                required
            },
            amount: {
                required
            },
 
            
        }


    },
    methods: {

        getEmployees() {

            axios.get('/PayrollDataEntry/GetEmployees').then(response => {

                response.data.forEach(v => {

                   v.employeeName = v.otherName == '' || null ? `${v.lastName} ${v.firstName} - ${v.employeeIdentifier}` : `${v.lastName} ${v.firstName} ${v.otherName} - ${v.employeeIdentifier}`

                });

                this.employeeList = response.data.sort((a, b) => a.lastName.localeCompare(b.lastName));

                $("#employeeId").ejDropDownList({
                    dataSource: response.data,
                    watermarkText: "Select Employee",
                    width: "100%",
                    fields: { id: "id", text: "employeeName", value: "id" },
                    enableFilterSearch: true,
                    change: (args) => {

                        this.employeeId = args.value;
                    }

                });


            }).catch(err => {

                //console.log(err);
            });
        },
 
        getLoanTypes() {

            axios.get('/PayrollSetups/GetLoanTypes').then(response => {
 
                this.loanTypeList = response.data
                
            }).catch(err => {

                //console.log(err);
            });

        },

        saveEmployeeSalaryAdvance() {


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

            let payload = {};

            if (!this.employeeId) {

                this.toastHandler('warning', 'Select or Search an employee');

            }

            this.isLoading = true;

            payload.employeeId = this.employeeId;

            let employeeInfo = this.getEmployeeDetails(this.employeeId);

            payload.employeeIdentifier = employeeInfo.employeeIdentifier;

            payload.employeeName = employeeInfo.employeeName;

            payload.loanDate = this.loanDate;

            payload.loanTypeId = this.loanTypeId;

            let loanInfo = this.getLoanTypeDetails(this.loanTypeId);

            payload.loanTypeName = loanInfo.name;
            payload.amount = this.amount;
            

            axios.post('/PayrollDataEntry/CreateEmployeeSalaryAdvance', payload).then(response => {

                this.toastHandler('', 'Salary advance saved.');
                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Unable to save employee loan.');
                this.isLoading = false;

            });
            
            
        },
        
        getLoanTypeDetails(id) {

            return this.loanTypeList.find(a => a.id === id);
        },

        getEmployeeDetails(id) {

            return this.employeeList.find(a => a.id === id);
        },

        initControls() {


            $( () => {


                $("#loanDate").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        if (args.value) {

                            this.loanDate = args.value
                        }
                    }

                });

                


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

        
    },
    watch: {


    },
    created() {

        this.initControls();
        this.getEmployees();
        this.getLoanTypes();

    },
});