
var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        nationalityList: [],
        currencyList: [],
        payload: {},
        companyName: '',
        countryId: '',
        cityName: '',
        phoneNo: '',
        companyTin: '',
        email: '',
        address: '',
        firstMonthOfFiscalYear: 1,
        senderId: '',
        clientId: '',
        clientSecret: '',
        employerSocialSecurityNo: '',
        socialSecurityFundEmployerRate: 0,
        socialSecurityFundEmployeeRate: 0,
        paySocialSecurityFund: false,
        providentFundEmployeeRate: 0,
        providentFundEmployerRate: 0,
        providentFundTreatAsSocialSecurityFund: false,
        payProvidentFund: false,
        providentFundTaxEmployerPortion: false,
        secondProvidentEmployeeRate: '',
        secondProvidentEmployerRate: '',
        paySecondProvidentOrOthers: false,
        treatSecondProvidentAsSSF: false,
        secondProvidentTaxEmployerPortion: false,
        overtimePercentageOnBasicSalary: false,
        overtimePercentageOnDailyWage: false,
        mailHostName: '',
        primaryEmailAddress: '',
        emailPassword: '',
        portNumber: 0,
        isSSLEnabled: false,
        companyProfileId: '',
        defaultCurrenyId: '',
        tenantId: '',

    },

    methods: {

         
        getBusinessProfile() {

            axios.get('/SharedResource/GetBusinessProfile').then(response => {

                if (response.data) {

                    let businessProfile = response.data;
                    this.companyName = businessProfile.companyName;
                    this.countryId = businessProfile.countryId;
                    this.cityName = businessProfile.cityName;
                    this.phoneNo = businessProfile.phoneNo;
                    this.companyTin = businessProfile.companyTin;
                    this.email = businessProfile.email;
                    this.address = businessProfile.address;
                    this.firstMonthOfFiscalYear = businessProfile.firstMonthOfFiscalYear;
                    this.senderId = businessProfile.senderId;
                    this.clientId = businessProfile.clientId;
                    this.clientSecret = businessProfile.clientSecret;
                    this.employerSocialSecurityNo = businessProfile.employerSocialSecurityNo;
                    this.socialSecurityFundEmployerRate = businessProfile.socialSecurityFundEmployerRate;
                    this.socialSecurityFundEmployeeRate = businessProfile.socialSecurityFundEmployeeRate;
                    this.paySocialSecurityFund = businessProfile.paySocialSecurityFund;
                    this.providentFundEmployeeRate = businessProfile.providentFundEmployeeRate;
                    this.providentFundEmployerRate = businessProfile.providentFundEmployerRate;
                    this.providentFundTreatAsSocialSecurityFund = businessProfile.providentFundTreatAsSocialSecurityFund;
                    this.payProvidentFund = businessProfile.payProvidentFund;
                    this.providentFundTaxEmployerPortion = businessProfile.providentFundTaxEmployerPortion;
                    this.secondProvidentEmployeeRate = businessProfile.secondProvidentEmployeeRate;
                    this.secondProvidentEmployerRate = businessProfile.secondProvidentEmployerRate;
                    this.paySecondProvidentOrOthers = businessProfile.paySecondProvidentOrOthers;
                    this.treatSecondProvidentAsSSF = businessProfile.treatSecondProvidentAsSSF;
                    this.secondProvidentTaxEmployerPortion = businessProfile.secondProvidentTaxEmployerPortion;
                    this.overtimePercentageOnBasicSalary = businessProfile.overtimePercentageOnBasicSalary;
                    this.overtimePercentageOnDailyWage = businessProfile.overtimePercentageOnDailyWage;
                    this.mailHostName = businessProfile.mailHostName;
                    this.primaryEmailAddress = businessProfile.primaryEmailAddress;
                    this.emailPassword = businessProfile.emailPassword;
                    this.portNumber = businessProfile.portNumber;
                    this.isSSLEnabled = businessProfile.isSSLEnabled;
                    this.companyProfileId = businessProfile.id;
                    this.defaultCurrenyId = businessProfile.defaultCurrenyId;
                    this.tenantId = businessProfile.tenantId;

                }

            }).catch(err => {

                console.log(err);

            });
        },

        getNationalities() {

            axios.get('/SharedResource/GetCountries').then(response => {

                this.nationalityList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get nationalities.');

            });
        },

        getCurrency() {

            axios.get('/SharedResource/GetCurrencys').then(response => {

                this.currencyList = response.data;

            }).catch(err => {

                this.toastHandler('error', 'Unable to get currency.');

            });
        },

        saveProfile() {

            let payload = {};

            payload.companyName = this.companyName;
            payload.countryId = this.countryId;
            payload.cityName = this.cityName;
            payload.phoneNo = this.phoneNo;
            payload.companyTin = this.companyTin;
            payload.email = this.email;
            payload.address = this.address;
            payload.firstMonthOfFiscalYear = this.firstMonthOfFiscalYear;
            payload.senderId = this.senderId;
            payload.clientId = this.clientId;
            payload.clientSecret = this.clientSecret;
            payload.employerSocialSecurityNo = this.employerSocialSecurityNo;
            payload.socialSecurityFundEmployerRate = this.socialSecurityFundEmployerRate;
            payload.socialSecurityFundEmployeeRate = this.socialSecurityFundEmployeeRate;
            payload.paySocialSecurityFund = this.paySocialSecurityFund;
            payload.providentFundEmployeeRate = this.providentFundEmployeeRate;
            payload.providentFundEmployerRate = this.providentFundEmployerRate;
            payload.providentFundTreatAsSocialSecurityFund = this.providentFundTreatAsSocialSecurityFund;
            payload.payProvidentFund = this.payProvidentFund;
            payload.providentFundTaxEmployerPortion = this.providentFundTaxEmployerPortion;
            payload.secondProvidentEmployeeRate = this.secondProvidentEmployeeRate;
            payload.secondProvidentEmployerRate = this.secondProvidentEmployerRate;
            payload.paySecondProvidentOrOthers = this.paySecondProvidentOrOthers;
            payload.treatSecondProvidentAsSSF = this.treatSecondProvidentAsSSF;
            payload.secondProvidentTaxEmployerPortion = this.secondProvidentTaxEmployerPortion;
            payload.overtimePercentageOnBasicSalary = this.overtimePercentageOnBasicSalary;
            payload.overtimePercentageOnDailyWage = this.overtimePercentageOnDailyWage;
            payload.mailHostName = this.mailHostName;
            payload.primaryEmailAddress = this.primaryEmailAddress;
            payload.emailPassword = this.emailPassword;
            payload.portNumber = this.portNumber;
            payload.isSSLEnabled = this.isSSLEnabled;
            payload.defaultCurrenyId = this.defaultCurrenyId;
            

            if (this.companyProfileId) {

                payload.id = this.companyProfileId;
                payload.tenantId = this.tenantId;

                axios.post('/SharedResource/UpdateBusinessProfile', payload).then(response => {

                    this.toastHandler('', 'Business profile info updated.');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to update business info.');

                });
            }
            else
            {

                axios.post('/SharedResource/CreateBusinessProfile', payload).then(response => {

                    this.toastHandler('', 'Business profile info saved.');

                }).catch(err => {

                    this.toastHandler('error', 'Unable to save business profile.');

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



    },
    created() {

        this.getBusinessProfile();

        this.getNationalities();

        this.getCurrency();

    },
});