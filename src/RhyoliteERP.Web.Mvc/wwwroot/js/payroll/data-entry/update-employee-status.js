Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

 

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        employeeList: [],
        employeeId: '',
        statusId: '',
        statusList: [],
         


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
 
        getEmployeeStatus() {

            axios.get('/PayrollSetups/GetEmployeeStatus').then(response => {

                this.statusList = response.data ;

            }).catch(err => {

                console.log(err);
            });
        },

        updateEmployeeStatus() {

            if (!this.employeeId) {

                this.toastHandler('warning', 'Select or Search an employee');
            }

            this.isLoading = true;

            axios.get(`/PayrollDataEntry/UpdateStatus?employeeId=${this.employeeId}&statusId=${this.statusId}`).then(response => {

                this.toastHandler('', 'Employee Status updated.');

                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Unable to update employee status.');
                this.isLoading = false;

            });
            
            
        },
        
        getEmployeeDetails(id) {

            return this.employeeList.find(a => a.id === id);
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

        this.getEmployees();
        this.getEmployeeStatus();

    },
});