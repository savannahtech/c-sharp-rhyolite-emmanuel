
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        payload: {},
        studentTotal: 0,
        staffTotal: 0,
        smsTopupAmount: null,
        subscriptionFeeAmount: null,

    },

    methods: {

        getStudentDistribution() {

            axios.get('/Dashboard/GetStudentGenderDistribution').then(response => {

                this.studentTotal = parseInt(response.data.maleInfo.nos) + parseInt(response.data.femaleInfo.nos);
                this.renderStudentDistributionChart(response.data);

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get data');

            });
        },

        renderStudentDistributionChart(objData) {

            var options = {
                chart: {
                    type: 'donut',
                    height: '340px',
                },
                labels: [objData.maleInfo.label, objData.femaleInfo.label,],
                series: [parseInt(objData.maleInfo.nos), parseInt(objData.femaleInfo.nos)],
                colors: ['#6956CE', '#1CD3D2'],
                dataLabels: {
                    enabled: false,
                },
                responsive: [{
                    breakpoint: 480,
                    options: {
                        legend: {
                            position: 'bottom'
                        }
                    }
                }]
            }
            var chart = new ApexCharts(
                document.querySelector("#mfdist-chart"),
                options
            );
            chart.render();
        },

        getStaffDistribution() {

            axios.get('/Dashboard/GetStaffGenderDistribution').then(response => {

                this.staffTotal = parseInt(response.data.maleInfo.nos) + parseInt(response.data.femaleInfo.nos);

                this.renderStaffDistributionChart(response.data);

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get data');

            });
        },
        renderStaffDistributionChart(objData) {

            var options = {
                chart: {
                    type: 'donut',
                    height: '340px',
                },
                labels: [objData.maleInfo.label, objData.femaleInfo.label,],
                series: [parseInt(objData.maleInfo.nos), parseInt(objData.femaleInfo.nos)],
                colors: ['#6956CE', '#4788ff'],
                dataLabels: {
                    enabled: false,
                },
                responsive: [{
                    breakpoint: 480,
                    options: {
                        legend: {
                            position: 'bottom'
                        }
                    }
                }]
            }
            var chart = new ApexCharts(
                document.querySelector("#mfstfdist-chart"),
                options
            );
            chart.render();
        },

        getPayments() {

            axios.get('/Dashboard/GetPayments').then(response => {

                var categories = [];
                var series = [];
                var billArray = [];
                var payArray = [];
                var payArreas = [];

                for (var i = 0; i < response.data.length; i++) {

                    categories.push(response.data[i].termName);
                    billArray.push(response.data[i].totalBills);
                    payArray.push(response.data[i].totalPayments);
                    payArreas.push(response.data[i].totalArreas);

                }

                series.push({ name: "Bills", data: billArray });
                series.push({ name: "Payments", data: payArray });
                series.push({ name: "Arreas", data: payArreas });

                this.renderPaymentsChart(series, categories);

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get payments data');

            });
        },
        renderPaymentsChart(seriesData, categoryData) {

            var options = {
                chart: {
                    height: 305,
                    type: 'bar',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '50%',
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    show: true,
                    width: 2,
                    colors: ['transparent']
                },
                colors: ['#ea3a3b', '#4788ff', '#6a4ffc'],
                series: seriesData,
                xaxis: {
                    categories: categoryData,
                },
                fill: {
                    opacity: 1
                },
                tooltip: {
                    y: {
                        formatter: function (val) {
                            return "GHC " + val;
                        }
                    }
                }
            }
            var chart = new ApexCharts(
                document.querySelector("#bp-chart"),
                options
            );
            chart.render();

        },
        getMonthlyPayments() {

            axios.get('/Dashboard/GetMonthlyPayments').then(response => {

                var categories = [];
                var series = [];
                var billArray = [];
                var payArray = [];
                var payArreas = [];

                for (var i = 0; i < response.data.length; i++) {

                    categories.push(response.data[i].month);
                    billArray.push(response.data[i].totalBills);
                    payArray.push(response.data[i].totalPayments);
                    payArreas.push(response.data[i].totalArreas);

                }

                series.push({ name: "Bills", data: billArray });
                series.push({ name: "Payments", data: payArray });
                series.push({ name: "Arreas", data: payArreas });

                this.renderMonthlyPaymentChart(series, categories);

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get monthly payments.');

            });
        },

        renderMonthlyPaymentChart(seriesData, categoryData) {

            var options = {
                chart: {
                    height: 305,
                    type: 'bar',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '50%',
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    show: true,
                    width: 2,
                    colors: ['transparent']
                },
                colors: ['#ea3a3b', '#4788ff', '#6a4ffc'],
                series: seriesData,
                xaxis: {
                    categories: categoryData,
                },
                fill: {
                    opacity: 1
                },
                tooltip: {
                    y: {
                        formatter: function (val) {
                            return "GHC " + val
                        }
                    }
                }
            }
            var chart = new ApexCharts(
                document.querySelector("#bp-chart2"),
                options
            );
            chart.render();

        },
        getPaymentsTillDate() {

            axios.get('/Dashboard/GetPaymentsTillDate').then(response => {

                var categories = [];
                var series = [];
                var billArray = [];
                var payArray = [];
                var payArreas = [];
                for (var i = 0; i < response.data.length; i++) {

                    categories.push(response.data[i].year);
                    billArray.push(response.data[i].totalBills);
                    payArray.push(response.data[i].totalPayments);
                    payArreas.push(response.data[i].totalArreas);

                }

                series.push({ name: "Bills", data: billArray });
                series.push({ name: "Payments", data: payArray });
                series.push({ name: "Arreas", data: payArreas });

                this.renderPaymentsTillDateChart(series, categories);

            }).catch(err => {

                //this.toastHandler('error', 'Unable to get payments data.');

            });
        },
        renderPaymentsTillDateChart(series, categories) {

            var options = {
                chart: {
                    height: 305,
                    type: 'bar',
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '50%',
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    show: true,
                    width: 2,
                    colors: ['transparent']
                },
                colors: ['#ea3a3b', '#4788ff', '#6a4ffc'],
                series: series,
                xaxis: {
                    categories: categories,
                },
                fill: {
                    opacity: 1
                },
                tooltip: {
                    y: {
                        formatter: function (val) {
                            return "GHC " + val
                        }
                    }
                }
            }
            var chart = new ApexCharts(
                document.querySelector("#bp-chart3"),
                options
            );
            chart.render();

        },

        smsAccountTopup() {

            if (this.smsTopupAmount == null || this.smsTopupAmount < 1) {

                this.$refs.smsTopupAmount.focus();

                return;
            }

            this.isLoading = true;

            this.payload = {};
            this.payload.Amount = this.smsTopupAmount;
            this.payload.serviceType = 'sms';
            axios.post('/Manage/InitializePayment', this.payload).then(response => {


                if (response.data.status == 2000) {

                    this.toastHandler('', response.data.message);
                    window.location = response.data.returnUrl;
                }
                else {

                    this.toastHandler('error', 'Unable to process payment request.');
                }

                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Unable to initialize payment.');
                this.isLoading = false;

            });
        },

        paySubscriptionFee() {

            if (this.subscriptionFeeAmount == null || this.subscriptionFeeAmount < 1) {

                this.$refs.subscriptionFeeAmount.focus();

                return;
            }

            this.isLoading = true;

            this.payload = {};
            this.payload.Amount = this.subscriptionFeeAmount;
            this.payload.serviceType = 'erp';

            axios.post('/Manage/InitializePayment', this.payload).then(response => {


                if (response.data.status == 2000) {

                    this.toastHandler('', response.data.message);
                    window.location = response.data.returnUrl;
                }
                else {

                    this.toastHandler('error', 'Unable to process payment request.');
                }

                this.isLoading = false;

            }).catch(err => {

                this.toastHandler('error', 'Unable to initialize payment.');
                this.isLoading = false;

            });
        },

        createTicket() {



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

        this.getStudentDistribution();
        this.getStaffDistribution();
        this.getPayments();
        this.getMonthlyPayments();
        this.getPaymentsTillDate();
        
    },
});