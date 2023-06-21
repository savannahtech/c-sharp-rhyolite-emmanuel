
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        tokenExpressionList: [

            { 'id': 1, 'expression': '[LoanBalance]', 'description': 'Current loan balance till date' },
            { 'id': 2, 'expression': '[LoanType]', 'description': 'Type of loan requested by employee' },
            { 'id': 3, 'expression': '[AccountNo]', 'description': 'Masked Account Number of Employee' },
            { 'id': 4, 'expression': '[BankName]', 'description': 'Current employee bank' },
            { 'id': 5, 'expression': '[BankBranchName]', 'description': 'Current employee bank branch' },
            { 'id': 6, 'expression': '[PayType]', 'description': 'Mode of salary payment for employee' },
            { 'id': 7, 'expression': '[SocialSecurityNo]', 'description': 'Employee Social Security Number' },
            { 'id': 8, 'expression': '[EmployeeName]', 'description': 'Employee full name' },
            { 'id': 9, 'expression': '[EmployeeID]', 'description': 'Employee ID' },
            { 'id': 10, 'expression': '[EmployeeCategory]', 'description': 'Current Employee Category' },
            { 'id': 11, 'expression': '[EmployeeDepartment]', 'description': 'Current Employee Department' },

        ],
    },

    methods: {

        copyToken(token) {

            navigator.clipboard.writeText(token);
        },

        renderTable() {

            $("#dataTable").ejGrid({

                dataSource: this.tokenExpressionList,
                allowPaging: true,
                isResponsive: true,
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "expression", headerText: "Expression", width: 55, },
                    { field: "description", headerText: "Description", width: 85, },

                ]
                
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

    created() {

        //this.renderTable();

    },
});