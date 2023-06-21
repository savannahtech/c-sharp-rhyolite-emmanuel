Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

 

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        employeeId: '',
        id: '',
        tenantId: '',
        socialSecurityNo: '',
        socialSecurityFundEmployerContribution: 0,
        socialSecurityFundEmployeeContribution: 0,
        providentFundEmployeeContribution: 0,
        providentFundEmployerContribution: 0,
        secondProvidentFundEmployeeContribution: 0,
        secondProvidentFundEmployerContribution: 0,
        superAnnuationEmployeeContribution: 0,
        superAnnuationEmployerContribution: 0,
        employeeInfo: {}

    },
    validations() {

        return {

            socialSecurityNo: {
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
 
         
        getSsnitInfo() {

            axios.get('/PayrollDataEntry/GetEmployeeSsnitInfo?employeeId='+this.employeeId).then(response => {

                if (response.data) {

                    let employeeSsnitInfo = response.data;

                    this.socialSecurityNo = employeeSsnitInfo.socialSecurityNo;
                    this.socialSecurityFundEmployerContribution = employeeSsnitInfo.socialSecurityFundEmployerContribution;
                    this.socialSecurityFundEmployeeContribution = employeeSsnitInfo.socialSecurityFundEmployeeContribution;
                    this.providentFundEmployeeContribution = employeeSsnitInfo.providentFundEmployeeContribution;
                    this.providentFundEmployerContribution = employeeSsnitInfo.providentFundEmployerContribution;
                    this.secondProvidentFundEmployeeContribution = employeeSsnitInfo.secondProvidentFundEmployeeContribution;
                    this.secondProvidentFundEmployerContribution = employeeSsnitInfo.secondProvidentFundEmployerContribution;
                    this.superAnnuationEmployeeContribution = employeeSsnitInfo.superAnnuationEmployeeContribution;
                    this.superAnnuationEmployerContribution = employeeSsnitInfo.superAnnuationEmployerContribution;
                    this.id = employeeSsnitInfo.id;
                    this.tenantId = employeeSsnitInfo.tenantId;

                    $("#ssfEmployer").ejNumericTextbox({ value: employeeSsnitInfo.socialSecurityFundEmployerContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#ssfEmployee").ejNumericTextbox({ value: employeeSsnitInfo.socialSecurityFundEmployeeContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#pfEmployee").ejNumericTextbox({ value: employeeSsnitInfo.providentFundEmployeeContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#pfEmployer").ejNumericTextbox({ value: employeeSsnitInfo.providentFundEmployerContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#secPFEmployee").ejNumericTextbox({ value: employeeSsnitInfo.secondProvidentFundEmployeeContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#secPFEmployer").ejNumericTextbox({ value: employeeSsnitInfo.secondProvidentFundEmployerContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#saEmployee").ejNumericTextbox({ value: employeeSsnitInfo.superAnnuationEmployeeContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                    $("#saEmployer").ejNumericTextbox({ value: employeeSsnitInfo.superAnnuationEmployerContribution, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });


                }
                else {

                    this.socialSecurityNo = '';
                    this.initControls();
                }
                
            }).catch(err => {

                //console.log(err);
            });

        },

        saveSnitInfo() {

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

            payload.socialSecurityNo = this.socialSecurityNo;
            payload.socialSecurityFundEmployerContribution = this.socialSecurityFundEmployerContribution;
            payload.socialSecurityFundEmployeeContribution = this.socialSecurityFundEmployeeContribution;
            payload.providentFundEmployeeContribution = this.providentFundEmployeeContribution;
            payload.providentFundEmployerContribution = this.providentFundEmployerContribution;
            payload.secondProvidentFundEmployeeContribution = this.secondProvidentFundEmployeeContribution;
            payload.secondProvidentFundEmployerContribution = this.secondProvidentFundEmployerContribution;
            payload.superAnnuationEmployeeContribution = this.superAnnuationEmployeeContribution;
            payload.superAnnuationEmployerContribution = this.superAnnuationEmployerContribution;
            payload.employeeId = this.employeeId;

            payload.employeeIdentifier = this.employeeInfo.employeeIdentifier;
            payload.employeeName = this.employeeInfo.employeeName;


            if (this.id) {

                payload.id = this.id;
                payload.tenantId = this.tenantId;

                axios.post('/PayrollDataEntry/UpdateEmployeeSsnit', payload).then(response => {

                    this.toastHandler('', 'Employee SSNIT info saved.');
                    this.id = '';

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save employee SSNIT info.');

                });
            }
            else {

                axios.post('/PayrollDataEntry/CreateEmployeeSsnit', payload).then(response => {

                    this.toastHandler('', 'Employee SSNIT info saved.');
                    this.id = '';


                }).catch(err => {

                    this.toastHandler('error', 'Unable to save employee SSNIT info.');

                });

            }
            
        },

        setCurrentEmployee() {

            this.employeeInfo = this.getEmployeeDetails(this.employeeId);

        },
        getEmployeeDetails(id) {

            return this.employeeList.find(a => a.id === id);
        },

        initControls() {


            $(function () {

                $("#ssfEmployer").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#ssfEmployee").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#pfEmployee").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#pfEmployer").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#secPFEmployee").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#secPFEmployer").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#saEmployee").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

                $("#saEmployer").ejNumericTextbox({ value: 0, minValue: 0, maxValue: 100, decimalPlaces: 2, watermarkText: "0.00", width: 'inherit', });

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

        employeeId(val) {

            if (val) {

                this.setCurrentEmployee();
                this.getSsnitInfo();

            }

        }

    },
    created() {

        this.initControls();
        this.getEmployees();

    },
});