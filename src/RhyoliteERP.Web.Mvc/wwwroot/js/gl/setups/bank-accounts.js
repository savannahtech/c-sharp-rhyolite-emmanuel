﻿Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        emptyGuid: '00000000-0000-0000-0000-000000000000',
        payload: {},
        bankList: [],
        bankBranchList: [],
        currencyList: [],
        cityList: [],
        accountList: [],
        bankAccountId: '',
        tenantId: 0,
        bankId: '',
        cityId: '',
        bankBranchId: '',
        accountId: '',
        accountName: '',
        address: '',
        location: '',
        accountNo: '',
        accountType: 'Current Account',
        currencyId: '',
        contactPerson: '',
        contactNo: '',
        chequeBookStart: '',
        chequeBookEnd: '',
        dateOpened: '',

    },
    validations() {

        return {

            bankId: {
                required
            },
            currencyId: {
                required,
            },
            accountId: {
                required,
            },
            accountNo: {
                required,
            },
            chequeBookStart: {
                required,
            },
            chequeBookEnd: {
                required,
            },


        }


    },
    methods: {

        getBanks() {

            axios.get('/SharedResource/GetBanks').then(response => {

                this.bankList = response.data;

            }).catch(err => {

                console.log(err);
            });
        },

        getCities() {

            axios.get('/SharedResource/GetCities').then(response => {

                this.cityList = response.data

            }).catch(err => {

                console.log(err);

            });
        },

        getCurrency() {

            axios.get('/SharedResource/GetCurrencys').then(response => {

                this.currencyList = response.data

            }).catch(err => {

                console.log(err);

            });
        },

        getAccounts() {

            axios.get('/LedgerSetups/GetChartOfAccountDetails').then(response => {

                this.accountList = response.data.accountDetails;

            }).catch(err => {

                console.log(err);

            });
        },

        getSetupData() {

            axios.get('/LedgerSetups/GetBankAccounts').then(response => {

                response.data.forEach((account) => {

                    account.dateOpened = moment(account.dateOpened).format("YYYY-MMM-DD");

                });

                this.renderTable(response.data);

            }).catch(err => {

                this.toastHandler('error', 'Unable to get bank accounts');

            });
        },

        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: {  allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "bankName", headerText: "Bank Name", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "bankBranchName", headerText: "Bank Branch", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "accountNo", headerText: "Account No", validationRules: { required: true, minlength: 1 }, width: 80 },
                    { field: "accountType", headerText: "Account Type", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "currencyName", headerText: "Currency", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "dateOpened", headerText: "Date Opened", validationRules: { required: true, minlength: 1 }, width: 55 },
                    { field: "cityName", headerText: "City", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "location", headerText: "Location", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "address", visible: false, headerText: "address", width: 15, },
                    { field: "contactPerson", visible: false, headerText: "contactPerson", width: 15, },
                    { field: "contactNo", visible: false, headerText: "contactNo", width: 15, },
                    { field: "chequeBookStart", visible: false, headerText: "ChequeBookStart", width: 15, },
                    { field: "chequeBookEnd", visible: false, headerText: "ChequeBookEnd", width: 15, },
                    { field: "currencyId", visible: false, headerText: "currencyId", width: 15, },
                    { field: "bankId", visible: false, headerText: "bankId", width: 15, },
                    { field: "bankBranchId", visible: false, headerText: "bankBranchId", width: 15, },
                    { field: "cityId", visible: false, headerText: "cityId", width: 15, },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },
                ],
                endEdit: (args) => {

                    this.updateBankAccount(args.data);
                },
                endDelete: (args) => {

                    this.delBankAccount(args.data.id);
                },
                recordDoubleClick: (args) => {

                    this.viewAccountDetails(args.data);
                }
            });

        },

        viewAccountDetails(bankAccountInfo) {

            this.bankAccountId = bankAccountInfo.id;
            this.bankId = bankAccountInfo.bankId;
            this.cityId = bankAccountInfo.cityId;
            this.bankBranchId = bankAccountInfo.bankBranchId;
            this.accountId = bankAccountInfo.accountId;
            this.address = bankAccountInfo.address;
            this.location = bankAccountInfo.location;
            this.accountNo = bankAccountInfo.accountNo;
            this.accountType = bankAccountInfo.accountType;
            this.currencyId = bankAccountInfo.currencyId;
            this.contactPerson = bankAccountInfo.contactPerson;
            this.contactNo = bankAccountInfo.contactNo;
            this.chequeBookStart = bankAccountInfo.chequeBookStart;
            this.chequeBookEnd = bankAccountInfo.chequeBookEnd;
            this.tenantId = bankAccountInfo.tenantId;
            this.dateOpened = moment(bankAccountInfo.dateOpened).format("YYYY-MM-DD");

            const addNewAccountBtn = this.$refs.addNewAccount;
            addNewAccountBtn.click();

        },

        saveBankAccount() {

            this.$v.$touch();


            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }

                }

                return;
            } 

            let payload = {};

            payload.bankId = this.bankId;
            let bankInfo = this.getBankDetails(this.bankId);
            payload.bankName = bankInfo.name;

            payload.cityId = this.emptyGuid;
            payload.cityName = '';
            payload.bankBranchId = this.emptyGuid;
            payload.bankBranchName = '';
            payload.accountId = this.emptyGuid;
            payload.accountName = '';
            payload.currencyId = this.emptyGuid;
            payload.currencyName = '';
            payload.currencyCode = '';

            if (this.cityId) {

                let cityInfo = this.getBankBranchDetails(this.bankBranchId);
                payload.cityName = cityInfo.name;

                payload.cityId = this.cityId;

            }

            if (this.bankBranchId) {

                let bankBranchInfo = this.getBankBranchDetails(this.bankBranchId);
                payload.bankBranchName = bankBranchInfo.name;
                payload.bankBranchId = this.bankBranchId;

            }

            if (this.accountId) {

                let accountInfo = this.getAccountDetails(this.accountId);
                payload.accountName = `${accountInfo.accountName} - ${accountInfo.accountNo}`;
                payload.accountId = this.accountId;

            }


            if (this.currencyId) {

                let currencyInfo = this.getCurrencyDetails(this.currencyId);
                payload.currencyName = currencyInfo.currencyName;
                payload.currencyCode = currencyInfo.currencyCode;

                payload.currencyId = this.currencyId;

            }

            payload.address = this.address;
            payload.location = this.location;
            payload.accountNo = this.accountNo;
            payload.accountType = this.accountType;
           
            payload.contactPerson = this.contactPerson;
            payload.contactNo = this.contactNo;
            payload.chequeBookStart = this.chequeBookStart;
            payload.chequeBookEnd = this.chequeBookEnd;
            payload.dateOpened = this.dateOpened;

            if (!this.dateOpened) {

                payload.dateOpened = new Date();
            }


            if (this.bankAccountId) {

                payload.id = this.bankAccountId;
                payload.tenantId = this.tenantId;

                axios.post('/LedgerSetups/UpdateBankAccount', payload).then(response => {

                    this.getSetupData();

                    this.toastHandler('', 'Bank account updated.');

                    this.bankAccountId = null;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to update bank account.');

                });

            } else {

                axios.post('/LedgerSetups/CreateBankAccount', payload).then(response => {

                    this.getSetupData();

                    this.toastHandler('', 'Bank Account saved.');
                    this.bankAccountId = null;

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save bank account.');

                });
            }
           
        },

        updateBankAccount(payload) {

            axios.post('/LedgerSetups/UpdateBankAccount', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Bank account updated.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to update bank account.');

            });
        },

        delBankAccount(id) {

            axios.get('/LedgerSetups/DelBankAccount?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Bank Account deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete bank account.');

            });
        },

        getBankDetails(id) {

            return this.bankList.find(a => a.id === id);
        },

        getBankBranchDetails(id) {

            return this.bankBranchList.find(a => a.id === id);
        },

        getCityDetails(id) {

            return this.cityList.find(a => a.id === id);
        },

        getAccountDetails(id) {

            return this.accountList.find(a => a.id === id);
        },

        getCurrencyDetails(id) {

            return this.currencyList.find(a => a.id === id);
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

        bankId(val) {

            if (val) {

                let bankInfo = this.getBankDetails(val);
                if (bankInfo) {
                    this.bankBranchList = bankInfo.bankBranches;
                    this.contactPerson = bankInfo.contactPerson;
                    this.contactNo = bankInfo.contactNo;
                }
                
            }

        },

        bankBranchId(val) {

            if (val) {

                let bankBranchInfo = this.getBankBranchDetails(val);

                if (bankBranchInfo) {

                    this.contactPerson = bankBranchInfo.contactPerson;
                    this.contactNo = bankBranchInfo.contactNo;
                }
                
            }
        }

    },
    created() {

        this.getSetupData();
        this.getBanks();
        this.getCurrency();
        this.getCities();
        this.getAccounts();
    },
});