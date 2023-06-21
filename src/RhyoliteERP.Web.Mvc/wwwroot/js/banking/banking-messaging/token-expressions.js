
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        payload: {},
        tokenExpressionList: [
            { 'id': 1, 'expression': '[LoanBalance]', 'description': 'Outstanding balance on recent loan' },
            { 'id': 6, 'expression': '[CustomerName]', 'description': 'Customer full name' },
            { 'id': 6, 'expression': '[CustomerFirstName]', 'description': 'Customer first name' },
            { 'id': 7, 'expression': '[TellerName]', 'description': 'Teller full name' },
            { 'id': 8, 'expression': '[TellerFirstName]', 'description': 'Teller first name' },


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