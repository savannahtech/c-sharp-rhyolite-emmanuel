
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        unitList: [],

    },     
     
    methods: {
        
        getUnitListings() {

            axios.get('/Rentals/GetUnitListings').then(response => {

                this.unitList = response.data;

                this.renderTable(response.data.sort((a, b) => a.unitNo.localeCompare(b.unitNo)));

            }).catch(err => {

                this.toastHandler('error', 'Unable to get tenants.');

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
                    { field: "unitNo", headerText: "Unit No", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "marketRent", headerText: "Market Rent", validationRules: { required: true, minlength: 1 }, format: "{0:n2}", width: 80 },
                    { field: "size", headerText: "Size", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "rooms", headerText: "Rooms", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "baths", headerText: "Baths", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "address", headerText: "Address", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],

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

        this.getUnitListings();
     

    },
});