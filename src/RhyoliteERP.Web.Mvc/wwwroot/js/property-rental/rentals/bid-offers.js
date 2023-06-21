
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
         
    },
    
    methods: {
        
        getBidOffers() {

            axios.get('/Rentals/GetBidOffers').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get bid offers.');

            });
        },

        
        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "fullName", headerText: "Full Name", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "phoneNo", headerText: "Phone No", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "offerAmount", headerText: "Offer Amount", format: "{0:n2}", width: 65 },
                    { field: "propertyName", headerText: "Property", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "propertyUnitNo", headerText: "Property/Unit No", width: 75 },
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
    
    created() {

        this.getBidOffers();
     

    },
});