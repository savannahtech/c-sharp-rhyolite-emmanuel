
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},

    },

    methods: {

        getStaff() {

            axios.get('/Staff/GetStaff').then(response => {

                response.data.forEach((v) => {

                    v.dateEmployed = this.formatDate(v.dateEmployed);
                })

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff.');

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
                    { field: "gender", headerText: "Gender", width: 75, visible: false, },
                    { field: "religionId", headerText: "ReligionId", width: 75, visible: false, },
                    { field: "homeAddress", headerText: "HomeAddress", width: 75, visible: false, },
                    { field: "staffIdentifier", headerText: "Staff ID", validationRules: { required: true, minlength: 1 }, width: 40, allowEditing: false },
                    { field: "lastName", headerText: "Last Name", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "firstName", headerText: "First Name", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "otherName", headerText: "Other Name", validationRules: { required: true, minlength: 1 }, width: 55, },
                    { field: "dateEmployed", headerText: "Date Employed", validationRules: { date: true }, width: 55, },
                    { field: "maritalStatus", headerText: "Marital Status", validationRules: { date: true }, width: 40, },
                    { field: "emailAddress", headerText: "Email", validationRules: { date: true }, width: 75, },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                recordDoubleClick: (args) => {

                    this.viewStaffDetails(args.data);
                },
                endDelete: (args) => {

                    this.delStaff(args.data.id);
                },
            });

        },
        viewStaffDetails(payload) {

            axios.get('/Staff/GetStaffDetails?id=' + payload.id).then(response => {

                console.log(response.data);

                //$("#stafffDetail").modal('show');


            }).catch(err => {

                this.toastHandler('error', 'Unable to get staff details.');

            });

        },
        delStaff(id) {

            axios.get('/Staff/DelStaff?id=' + id).then(response => {

                this.getStaff();

                this.toastHandler('', 'Student deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete staff.');

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

        this.getStaff();
    },
});