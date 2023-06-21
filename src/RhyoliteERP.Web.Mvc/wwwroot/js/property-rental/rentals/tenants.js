
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
        
        getTenants() {

            axios.get('/Rentals/GetLeaseTenants').then(response => {

                response.data.forEach((v) => {

                    v.unitName = !v.leasedPropertyUnitNo ? v.leasedPropertyName : v.leasedPropertyUnitNo

                });

                this.tenantList = response.data;

                console.log(response.data);

                this.renderTable(response.data);

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
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "firstName", headerText: "First Name", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "lastName", headerText: "Last Name", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "primaryPhoneNo", headerText: "Phone No", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "primaryEmail", headerText: "Email", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "unitName", headerText: "Property/Unit No", validationRules: { required: true, minlength: 1 }, width: 80 },
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

        this.getTenants();
     

    },
});