
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        customerList: [],
        customerGroupList: [],
        accountNumber: '',
        customerGroupId: '',
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
        selectedCustomerId: '',
        customerId: null,
        selectedCustomerGroupId: '',

    },

    methods: {


        getCustomers() {

            axios.get('/SharedResource/GetCustomers').then(response => {

                this.renderTable(response.data);
                this.customerList = response.data;


            }).catch(err => {

                this.toastHandler('error', 'Unable to get customers.');

            });
        },

        getCustomersByGroup() {

           
            axios.get('/SharedResource/GetCustomersByGroup?groupId=' + this.selectedCustomerGroupId).then(response => {

                this.customerList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get customers.');

            });
        },

        getCustomerGroups() {

            axios.get('/SharedResource/GetCustomerGroups').then(response => {

                this.customerGroupList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to customer group.');

            });
        },

        getCustomerDetails() {

            return this.customerList.find(a => a.id === id);

        },


        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Customer Name", width: 75 },
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

                    this.delCustomer(args.data.id);
                },
            });

        },

        saveCustomer() {

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

            if (this.customerId != null) {

                axios.post('/SharedResource/UpdateCustomer', payload).then(response => {

                    this.toastHandler('', 'Customer info updated.');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to update customer info.');

                });

            }
            else
            {

                axios.post('/SharedResource/CreateCustomer', payload).then(response => {

                    this.toastHandler('', 'Customer info saved.');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save customer info.');

                });
            }


            
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

        selectedCustomerGroupId(val) {

            if (!val) {

                this.getCustomers();
            }
            else {

                this.getCustomersByGroup();
            }

        }
    },
    created() {

        this.getCustomers();
        this.getCustomerGroups();
    },
});