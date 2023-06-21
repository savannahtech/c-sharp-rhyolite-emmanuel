
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getCustomerAccounts() {

            axios.get('/BankCustomers/GetCustomerAccounts').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get customer accounts.');

            });
        },

        renderTable(responseData) {

            $("#dataTable").ejGrid({
                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                allowGrouping: true,
                enableDropAreaAnimation: true,
                editSettings: { allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Search, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "classId", headerText: "ClassId", width: 75, visible: false, },
                    { field: "nationalityId", headerText: "NationalityId", width: 75, visible: false, },
                    { field: "enrollmentDate", headerText: "EnrollmentDate", width: 75, visible: false, },
                    { field: "gender", headerText: "Gender", width: 75, visible: false, },
                    { field: "religionId", headerText: "ReligionId", width: 75, visible: false, },
                    { field: "academicYearId", headerText: "AcademicYearId", width: 75, visible: false, },
                    { field: "homeAddress", headerText: "HomeAddress", width: 75, visible: false, },
                    { field: "cityOrLocation", headerText: "CityOrLocation", width: 75, visible: false, },
                    { field: "studImage", headerText: "StudImage", width: 75, visible: false, },
                    { field: "studentStatusId", headerText: "StudentStatus", width: 75, visible: false, },
                    { field: "studentIdentifier", headerText: "Student ID", validationRules: { required: true, minlength: 1 }, width: 50, allowEditing: false },
                    { field: "lastName", headerText: "Last Name", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "middleName", headerText: "Middle Name", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "firstName", headerText: "First Name", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "dateOfBirth", headerText: "Date Of Birth", validationRules: { date: true }, width: 55, },
                    { field: "className", headerText: "Class", validationRules: { date: true }, width: 40, },
                    { field: "studentStatusName", headerText: "Status", validationRules: { date: true }, width: 40, },

                ],
                recordDoubleClick: (args) => {

                    this.viewCustomer(args.data);
                },
                endDelete: (args) => {

                    this.delCustomer(args.data.id);
                },
            });

        },
        viewCustomer(payload) {

            axios.get('/BankCustomers/GetCustomerDetails?id=' + payload.id).then(response => {

                console.log(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get account details.');

            });

        },
        delCustomer(id) {

            axios.get('/BankCustomers/DelCustomerAccount?id=' + id).then(response => {

                this.getStudents();

                this.toastHandler('', 'Account deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete customer.');

            });
        },
        formatDate(dateInput) {

            return moment(dateInput).format("YYYY-MM-DD");

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

    created() {

        this.getCustomerAccounts();
    },
});