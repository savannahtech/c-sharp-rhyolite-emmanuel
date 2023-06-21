﻿ 

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        bankList: [],
        bankBranchList: []
        
    },

    methods: {
       
        getSetupData() {

            axios.get('/SharedResource/GetBanks').then(response => {


                this.bankList = response.data;

                this.bankList.forEach((bank) => {

                    bank.bankId = bank.id;

                    bank.bankBranches.forEach((branch) => {

                        branch.bankId = bank.id;

                        this.bankBranchList.push(branch);

                    })

                });

                this.renderTable(response.data, this.bankBranchList);


            }).catch(err => {

                this.toastHandler('error','Unable to get banks.');
                 
            });
        },
       
        renderTable(responseData)
        {

           $("#dataTable").ejGrid({
                    dataSource: responseData,
                    allowPaging: true,
                    allowSorting: true,
                    isResponsive: true,
                    editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                    toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                    childGrid: {
                        dataSource: this.bankBranchList,
                        queryString: "bankId",
                        allowPaging: true,
                        allowSorting: true,
                        isResponsive: true,
                        pageSettings: { pageSize: 10 },
                        editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                        toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                        columns: [

                            { field: "id", headerText: 'Id', visible: false, isPrimaryKey: true, width: 80 },
                            { field: "bankId", headerText: 'bankId', visible: false, width: 80 },
                            { field: "name", headerText: 'Bank Branch', textAlign: ej.TextAlign.Left, width: 60 },
                            { field: "contactPerson", headerText: "Contact Person", validationRules: { required: true, minlength: 1 }, width: 80 },
                            { field: "contactNo", headerText: "Contact No", validationRules: { required: true, minlength: 1 }, width: 65 },
                            { field: "email", headerText: "Email", validationRules: { required: true, minlength: 1 }, width: 80 },

                        ],
                        endAdd: (args) => {

                           this.saveBranch(args.data);
                        },
                        endEdit: (args) => {

                            this.updateBranch(args.data);
                        },
                        endDelete: (args) => {

                            this.delBranch(args.data.id);
                        },

                    },
               columns: [

                        { field: "id", headerText: "Id", width: 75, visible: false, isPrimaryKey: true, },
                        { field: "name", headerText: "Bank Name", validationRules: { required: true, minlength: 1 }, width: 80 },
                        { field: "contactPerson", headerText: "Contact Person", validationRules: { required: true, minlength: 1 }, width: 80 },
                        { field: "contactNo", headerText: "Contact No", validationRules: { required: true, minlength: 1 }, width: 65 },
                        { field: "email", headerText: "Email", validationRules: { required: true, minlength: 1 }, width: 80 },
                        { field: "bankCode", headerText: "Bank Code", validationRules: { required: true, minlength: 1 }, width: 65 },
                        { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                    ],
                    endAdd: (args) => {

                        this.saveBank(args.data);
                    },
                    endEdit: (args) => {

                        this.updateBank(args.data);
                    },
                    endDelete: (args) => {

                        this.deleteBank(args.data.id);
                    },
                });

        },

        saveBank(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/SharedResource/CreateBank', payload ).then(response => {

                this.getSetupData();


                if (response.data.code == 200) {
                    this.toastHandler('', 'Bank saved.');

                }
                else {
                    this.toastHandler('error', response.data.message);

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to save bank.');

            });
        },

        updateBank(payload) {

            axios.post('/SharedResource/UpdateBank', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Bank updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update bank.');

            });
        },

        deleteBank(id) {

            axios.get('/SharedResource/DelBank?id='+id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Bank deleted.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to delete bank.');

            });
        },

        saveBranch(payload) {

            payload.id = this.uuid();

            axios.post('/SharedResource/CreateBankBranch', payload).then(response => {

                if (response.data.code == 200) {
                    this.toastHandler('', 'Bank Branch saved.');

                }
                else {
                    this.toastHandler('error', response.data.message);

                }

            }).catch(err => {

                this.toastHandler('error', 'Unable to save bank branch.');

            });
        },

        updateBranch(payload) {

            axios.post('/SharedResource/UpdateBankBranch', payload).then(response => {

                this.toastHandler('', 'Bank Branch updated.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to update bank branch.');

            });
        },

        delBranch(payload) {

            axios.post('/SharedResource/DelBankBranch', payload).then(response => {

                this.toastHandler('', 'Bank Branch delete.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete bank branch.');

            });
        },

        getBankDetails(id) {

            return this.bankList.find(a => a.id === id);
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

        uuid() {

            return([1e7]+ -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
        }
    },
     
    created() {

        this.getSetupData();
    },
});