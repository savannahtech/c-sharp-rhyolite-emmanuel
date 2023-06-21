
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        supplierList: [],
        supplierGroupList: [],
        accountNumber: '',
        supplierGroupId: '',
        status: '',
        address: '',
        balance: 0,
        vatAccountId: '',
        name: '',
        tinNumber: '',
        phoneNo: '',
        email: '',
        currencyId: '',
        creditLimit: '',
        irsAccountId: '',
        selectedSupplierId: '',
        supplierId: null,
        selectedSuppplierGroupId: '',

    },

    methods: {


        getSuppliers() {

            axios.get('/SharedResource/GetSuppliers').then(response => {

                this.renderTable(response.data);
                this.supplierList = response.data;



            }).catch(err => {

                this.toastHandler('error', 'Unable to get suppliers.');

            });
        },

        getSuppliersByGroup() {


            axios.get('/SharedResource/GetSuppliersByGroup?groupId=' + this.selectedSuppplierGroupId).then(response => {

                this.supplierList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get suppliers.');

            });
        },

        getSupplierGroups() {

            axios.get('/SharedResource/GetSupplierGroups').then(response => {

                this.supplierGroupList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to suppliers.');

            });
        },

        getSupplierDetails() {

            return this.supplierList.find(a => a.id === id);
        },

        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Supplier Name", width: 75 },
                    { field: "phoneNo", headerText: "Phone No", width: 65 },
                    { field: "email", headerText: "Email", width: 65 },
                    { field: "tinNumber", headerText: "TIN No", width: 65 },
                    { field: "currencyName", headerText: "Default Currency", width: 65 },
                    { field: "debitAccountName", headerText: "Debit Account", width: 65 },
                    { field: "vatAccountName", headerText: "VAT Account", width: 65 },
                    { field: "creditAccountName", headerText: "Credit Account", width: 65 },
                    { field: "irsAccountName", headerText: "IRS Account", width: 65 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },
                ],
                 
                endDelete: (args) => {

                    this.delSupplier(args.data.id);
                },
            });

        },

        saveSupplier() {

            let payload = {};

            payload.accountNumber = this.accountNumber;
            payload.customerGroupId = this.customerGroupId;
            payload.customerGroupName = '';
            payload.status = this.status;
            payload.balance = this.balance;
            payload.vatAccountId = this.vatAccountId;
            payload.vatAccountName = '';
            payload.creditAccountId = this.creditAccountId;
            payload.creditAccountName = '';
            payload.tinNumber = this.tinNumber;
            payload.name = this.name;
            payload.email = this.email;
            payload.phoneNo = this.phoneNo;
            payload.address = this.address;
            payload.currencyId = this.currencyId;
            payload.currencyName = '';
            payload.currencyCode = '';
            payload.creditLimit = 0;
            payload.irsAccountId = '';
            payload.irsAccountName = '';
            payload.irsAccountName = '';

            if (this.supplierId != null) {

                axios.post('/SharedResource/UpdateSupplier', payload).then(response => {

                    this.toastHandler('', 'Supplier info updated.');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to update supplier.');

                });
            }
            else {

                axios.post('/SharedResource/CreateSupplier', payload).then(response => {

                    this.toastHandler('', 'Supplier info saved.');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save supplier.');

                });

            }
            
        },

        delSupplier(id) {

            axios.get('/SharedResource/DelSupplier?id=' + id).then(response => {

                this.getSuppliers();

                this.toastHandler('', 'Supplier info deleted.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to delete supplier.');

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

        selectedSuppplierGroupId(val) {

            if (!val) {

                this.getSuppliers();
            }
            else {

                this.getSuppliersByGroup();
            }

        }
    },
    created() {

        this.getSuppliers();
        this.getSupplierGroups();
    },
});