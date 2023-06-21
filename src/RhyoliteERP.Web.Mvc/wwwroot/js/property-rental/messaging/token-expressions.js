
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        tokenExpressionList: [

            { 'id': 1, 'expression': '[TenantFirstName]', 'description': 'Tenant First Name' },
            { 'id': 2, 'expression': '[TenantLastName]', 'description': 'Tenant Last Name' },
            { 'id': 3, 'expression': '[TenantFullName]', 'description': 'Tenant Full Name' },
            { 'id': 4, 'expression': '[ApplicantFirstName]', 'description': 'Applicant First Name' },
            { 'id': 5, 'expression': '[ApplicantLastName]', 'description': 'Applicant Last Name' },
            { 'id': 6, 'expression': '[ApplicantFullName]', 'description': 'Applicant Full Name' },
            { 'id': 7, 'expression': '[UnitNo]', 'description': 'Unit No' },
            { 'id': 8, 'expression': '[TenantEmail]', 'description': 'Tenant Email' },
            { 'id': 9, 'expression': '[ApplicantEmail]', 'description': 'Applicant Email' },
            { 'id': 10, 'expression': '[RentOrLeaseExpiryDate]', 'description': 'Rent Or Lease Expiry Date' },

        ],
    },

    methods: {

        copyToken(token) {

            navigator.clipboard.writeText(token);

            this.toastHandler('', 'Copied');

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