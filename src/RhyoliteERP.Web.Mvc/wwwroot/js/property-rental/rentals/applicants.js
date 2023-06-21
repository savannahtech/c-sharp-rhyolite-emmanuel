
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        applicantList: [],
        
         
    },
     
    methods: {
        
        getApplicants() {

            axios.get('/Rentals/GetLeaseApplicants').then(response => {

                this.applicantList = response.data;

                this.renderTable(response.data);

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get applicants.');

            });
        },

        
        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: {  allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "firstName", headerText: "First Name", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "lastName", headerText: "Last Name", validationRules: { required: true, minlength: 1 }, format: "{0:n2}", width: 75 },
                    { field: "email", headerText: "Email", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "primaryPhoneNo", headerText: "Primary Phone No", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "secondaryPhoneNo", headerText: "Secondary Phone No", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "propertyName", headerText: "Property", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "propertyUnitName", headerText: "Property Unit", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endDelete: (args) => {

                    this.delApplicant(args.data.id);
                },
            });

        },

        delApplicant(id) {

            axios.get('/Rentals/DelLeaseApplicant?id=' + id).then(response => {

                this.getApplicants();

                this.toastHandler('', 'Applicant deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete applicant.');

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

        this.getApplicants();
     

    },
});