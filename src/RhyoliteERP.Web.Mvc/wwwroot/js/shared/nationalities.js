﻿
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        countryList: [],
    },

    methods: {

        getSetupData() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.countryList = response.data;

                this.renderTable(response.data);


            }).catch(err => {

                this.toastHandler('error', 'Unable to get nationalities.');

            });
        },

        renderTable(responseData) {

            $("#dataTable").ejGrid({

                dataSource: responseData,
                allowPaging: true,
                allowSorting: true,
                isResponsive: true,
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, showDeleteConfirmDialog: true },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel, ej.Grid.ToolBarItems.Search] },
                columns: [

                    { field: "id", headerText: "id", width: 75, visible: false, isPrimaryKey: true, },
                    { field: "name", headerText: "Country", validationRules: { required: true, minlength: 1 }, width: 75 },
                    { field: "code", headerText: "Country Code", validationRules: { required: true, minlength: 1 }, width: 55 },
                    { field: "numericIsoCode", headerText: "Numeric Iso Code", editType: "numericedit", editParams: { decimalPlaces: 0 },validationRules: { required: true,} , width: 55 },
                    { field: "nationality", headerText: "Nationality", validationRules: { required: true, minlength: 1 }, width: 65 },
                    { field: "tenantId", visible: false, headerText: "tenantId", width: 15, },

                ],
                endAdd: (args) => {

                    this.saveCountry(args.data);
                },
                endEdit: (args) => {

                    this.updateCountry(args.data);
                },
                endDelete: (args) => {

                    this.delCountry(args.data.id);
                },
            });

        },

        saveCountry(payload) {

            delete payload.id;
            delete payload.tenantId;

            axios.post('/SharedResource/CreateCountry', payload).then(response => {

                this.getSetupData();
                this.toastHandler('', 'Nationality saved.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to save nationality.');

            });
        },

        updateCountry(payload) {


            axios.post('/SharedResource/UpdateCountry', payload).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Nationality updated.');


            }).catch(err => {

                this.toastHandler('error', 'Unable to update nationality.');

            });
        },

        delCountry(id) {

            axios.get('/SharedResource/DelCountry?id=' + id).then(response => {

                this.getSetupData();

                this.toastHandler('', 'Nationality deleted.');

            }).catch(err => {

                this.toastHandler('error', 'Unable to delete currency.');

            });
        },

        formatShortDate(dateInput) {

            return moment(dateInput).format("DD-MMM-YYYY");

        },
        uuid() {

            return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
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

        this.getSetupData();
    },
});