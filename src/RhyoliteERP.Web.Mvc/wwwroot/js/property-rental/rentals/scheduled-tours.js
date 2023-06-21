
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        countryList: [],
        ledgerAccountList: [],
        propertyGroupList: [],
        propertyTypeList: [],
        tenantList: [],
        name: '',
        propertyTypeId: '',
        address: '',
        regionOrState: '',
        propertyManager: '',
        propertyManagerPhoneNo: '',
        PropertyManagerEmail: '',
        propertyIdentifier: '',
        propertyGroupId: '',
        countryId: '',
        propertyReserve: 0,
        countryName: '',
        city: '',
        ledgerAccountId: '',
        propertyId: ''
         
    },
    
    methods: {
        
        getSheduledTours() {

            axios.get('/Rentals/GetSheduledTours').then(response => {

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get scheduled tours.');

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
                    { field: "customerName", headerText: "Customer Name", width: 80 },
                    { field: "email", headerText: "Email", width: 80 },
                    { field: "phoneNo", headerText: "Phone No", width: 65 },
                    { field: "scheduledDate", headerText: "Email", width: 70 },
                    { field: "propertyName", headerText: "Property", width: 80 },
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

        this.getSheduledTours();
     

    },
});