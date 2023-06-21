﻿Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

 

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        employeeList: [],
        employeeId: '',
        id: '',
        tenantId: '',
        loanTypeList: [],
        loanDate: '',
        loanTypeId: '',
        amount: null,
        monthlyDeduction: null,
        annualInterestRate: null,
        interestCharges: null,
        deductionStarts: '',
        nextDeduction: 1,
        duration: 1,
        currentBalance: null,
        interestType: 'No Interest',
        chargeInterest: false


    },
    validations() {

        return {

            loanTypeId: {
                required
            },

            loanDate: {
                required
            },

            deductionStarts: {
                required
            },

            amount: {
                required
            },

            duration: {
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

        saveEmployeeLoan() {


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
                return;
            }

            this.isLoading = true;

            payload.employeeId = this.employeeId;

            let employeeInfo = this.getEmployeeDetails(this.employeeId);

            payload.employeeIdentifier = employeeInfo.employeeIdentifier;

            payload.employeeName = employeeInfo.employeeName;

            payload.loanDate = this.loanDate;

            payload.deductionStarts = this.deductionStarts;

            payload.loanTypeId = this.loanTypeId;

            let loanInfo = this.getLoanTypeDetails(this.loanTypeId);

            payload.loanTypeName = loanInfo.name;
            payload.nextDeduction = this.nextDeduction;
            payload.amount = parseFloat(this.amount);
            payload.duration = this.duration;
            payload.monthlyDeduction = parseFloat(this.monthlyDeduction);
            payload.chargeInterest = loanInfo.chargeInterest;
            payload.currentBalance = this.currentBalance ? parseFloat(this.currentBalance) : 0;
            payload.annualInterestRate = this.annualInterestRate ? parseFloat(this.annualInterestRate) : 0;
            payload.interestType = this.interestType;
            payload.interestCharges = this.interestCharges ? parseFloat(this.interestCharges) : 0;
            

            axios.post('/PayrollDataEntry/CreateEmployeeLoan', payload).then(response => {

                this.toastHandler('', 'Employee Loan saved.');
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

                $("#deductionStarts").ejDatePicker({
                    width: "100%",
                    value: new Date(),
                    dateFormat: "dd-MMM-yyyy",
                    change: (args) => {
                        this.deductionStarts = args.value
                    }

                });


            });


        },

        calculateInterest() {

            let simpleInterest = 0;

            let timeInYears = this.duration / 12;

            if (this.amount && timeInYears != 0) {

                if (this.interestType == "Simple Interest") {

                    simpleInterest = (this.amount * this.annualInterestRate * timeInYears) / 100;

                    this.interestCharges = parseFloat(simpleInterest).toFixed(2);
                    this.currentBalance = parseFloat(parseFloat(simpleInterest) + parseFloat(this.amount)).toFixed(2);
                    this.monthlyDeduction = parseFloat(parseFloat(simpleInterest) + parseFloat(this.amount) / this.duration).toFixed(2);


                }
                else if (this.interestType == "Compound Interest") {

                    let compoundAmount = this.amount * (1 + (this.annualInterestRate / 1)) ** (timeInYears * 1);

                    let compoundInterest = compoundAmount - this.amount;

                    this.interestCharges = parseFloat(compoundInterest).toFixed(2);
                    this.currentBalance = parseFloat(compoundAmount).toFixed(2);
                    this.monthlyDeduction = parseFloat(parseFloat(compoundAmount) / this.duration).toFixed();


                }
                else {

                    this.interestCharges = 0;
                    this.currentBalance = parseFloat(this.amount).toFixed(2);
                    this.monthlyDeduction = parseFloat(parseFloat(this.amount) / this.duration).toFixed(2);

                }

            }

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

        loanTypeId(val) {

            if (val) {

                let loanInfo = this.getLoanTypeDetails(val);

                if (loanInfo) {

                    this.chargeInterest = loanInfo.chargeInterest;
                    this.interestType = loanInfo.chargeInterest ? 'Simple Interest' : 'No Interest';
                    this.annualInterestRate = loanInfo.chargeInterest ? 1 : 0;
                }
                

            }
        },
        duration(val) {

            if (val <= 0) {
                this.duration = 1;
            }

            this.calculateInterest();
        },
        amount(val) {

            if (val <= 0) {
                this.amount = 1;
            }
            this.calculateInterest();
        },
        annualInterestRate(val) {

            if (val < 0) {
                this.annualInterestRate = 1;
            }
            this.calculateInterest();
        },

        nextDeduction(val) {

            if (val <= 0) {
                this.nextDeduction = 1;
            }
        },
        interestType(val) {
            this.calculateInterest();

        }
    },
    created() {

        this.initControls();
        this.getEmployees();
        this.getLoanTypes();

    },
});