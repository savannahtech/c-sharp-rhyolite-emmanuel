using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RhyoliteERP.Subscription.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RhyoliteERP.Subscription
{
    public class SubscriptionAppService: RhyoliteERPAppServiceBase, ISubscriptionAppService
    {
        private readonly IConfiguration _configuration;

        public SubscriptionAppService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task<BalanceDto> GetSmsAccountBalance()
        {
            var url = $"{_configuration["SubscriptionManager:BaseUrl"]}api/services/app/SmsAccount/GetDetails";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);

            var request = new RestRequest()
                            .AddQueryParameter("tenantId", $"{AbpSession.TenantId}")
                            .AddQueryParameter("accountSource", "erp");

            var cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(80000);

            var response = await client.GetAsync<BalanceDto>(request, cancellationTokenSource.Token);
           
            Logger.Info($"sms account balance => {JsonConvert.SerializeObject(response)}");

            return response;
        }

        public async Task<object> ListInvoices(int pageNo, int pageSize)
        {

            var url = $"{_configuration["SubscriptionManager:BaseUrl"]}api/services/app/Invoice/ListTenantInvoices";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);

            var request = new RestRequest()
                            .AddQueryParameter("tenantId", $"{AbpSession.TenantId}")
                            .AddQueryParameter("accountSource", "erp")
                            .AddQueryParameter("pageNo", pageNo)
                            .AddQueryParameter("pageSize", pageSize);

            var cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(80000);

            var response = await client.GetAsync<InvoiceInfo>(request, cancellationTokenSource.Token);

            return response;

        }

        public async Task<List<SubscriptionSummaryDto>> ListSubscribedModules()
        {
            List<SubscriptionSummaryDto> subscriptionSummaryDtos = new List<SubscriptionSummaryDto>();

            var url = $"{_configuration["SubscriptionManager:BaseUrl"]}api/services/app/Subscription/ListSubscribedModules";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);

            var request = new RestRequest()
                        .AddQueryParameter("tenantId", $"{AbpSession.TenantId}")
                        .AddQueryParameter("accountSource", "erp");

            var cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(80000);

            var result = await client.GetAsync<BusinessSubscriptionInfo>(request, cancellationTokenSource.Token);

            Logger.Info($"subscription list by current business => {JsonConvert.SerializeObject(result)}");

            if (result?.Result != null)
            {
                if (result.Result.IsApSubscribed)
                {
                    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Accounts Payable", Description = RhyoliteERPConsts.AccountPayablesDescription, ModuleId = RhyoliteERPConsts.AccountPayables, Status = "Active", StatusCode = 0,  Fee = result.Result.ApFee, SubscriptionStartDate = result.Result.ApSubscriptionStartDate, SubscriptionEndDate = result.Result.ApNextRenewalDate, BillingCycle = result.Result.ApBillingCycle });
                }
                else
                {
                    if (DateTime.UtcNow > result.Result.ApSubscriptionEndDate)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Accounts Payable", Description = RhyoliteERPConsts.AccountPayablesDescription, ModuleId = RhyoliteERPConsts.AccountPayables, Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.ApFee, SubscriptionStartDate = result.Result.ApSubscriptionStartDate, SubscriptionEndDate = result.Result.ApNextRenewalDate, BillingCycle = result.Result.ApBillingCycle });
                    }
                    else if(result.Result.ApFee <= 0)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Accounts Payable", Description = RhyoliteERPConsts.AccountPayablesDescription,  ModuleId = RhyoliteERPConsts.AccountPayables, Status = "InActive", StatusCode = 2, Fee = result.Result.ApFee, SubscriptionStartDate = result.Result.ApSubscriptionStartDate, SubscriptionEndDate = result.Result.ApNextRenewalDate, BillingCycle = result.Result.ApBillingCycle });
                    }

                }

                if (result.Result.IsArSubscribed)
                {
                    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Accounts Receivable", Description = RhyoliteERPConsts.AccountReceivablesDescription, ModuleId = RhyoliteERPConsts.AccountReceivables, Status = "Active", StatusCode = 0, Fee = result.Result.ArFee, SubscriptionStartDate = result.Result.ArSubscriptionStartDate, SubscriptionEndDate = result.Result.ArNextRenewalDate, BillingCycle = result.Result.ArBillingCycle });
                }
                else
                {
                    if (DateTime.UtcNow > result.Result.ArSubscriptionEndDate)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Accounts Receivable", Description = RhyoliteERPConsts.AccountReceivablesDescription, ModuleId = RhyoliteERPConsts.AccountReceivables, Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.ArFee, SubscriptionStartDate = result.Result.ArSubscriptionStartDate, SubscriptionEndDate = result.Result.ArNextRenewalDate, BillingCycle = result.Result.ArBillingCycle });
                    }
                    else if (result.Result.ArFee <= 0)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Accounts Receivable", Description = RhyoliteERPConsts.AccountReceivablesDescription, ModuleId = RhyoliteERPConsts.AccountReceivables, Status = "InActive", StatusCode = 2, Fee = result.Result.ArFee, SubscriptionStartDate = result.Result.ArSubscriptionStartDate, SubscriptionEndDate = result.Result.ArNextRenewalDate, BillingCycle = result.Result.ArBillingCycle });
                    }
                }

                if (result.Result.IsGlSubscribed)
                {
                    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "General Ledger", Description = RhyoliteERPConsts.GeneralLedgerDescription, ModuleId = RhyoliteERPConsts.GeneralLedger, Status = "Active", StatusCode = 0, Fee = result.Result.GlFee, SubscriptionStartDate = result.Result.GlSubscriptionStartDate, SubscriptionEndDate = result.Result.GlNextRenewalDate, BillingCycle = result.Result.GlBillingCycle });
                }
                else
                {
                    if (DateTime.UtcNow > result.Result.GlSubscriptionEndDate)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "General Ledger", Description = RhyoliteERPConsts.GeneralLedgerDescription, ModuleId = RhyoliteERPConsts.GeneralLedger, Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.GlFee, SubscriptionStartDate = result.Result.GlSubscriptionStartDate, SubscriptionEndDate = result.Result.GlNextRenewalDate, BillingCycle = result.Result.GlBillingCycle });
                    }
                    else if(result.Result.GlFee <= 0)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "General Ledger", Description = RhyoliteERPConsts.GeneralLedgerDescription, ModuleId = RhyoliteERPConsts.GeneralLedger, Status = "InActive", StatusCode = 2, Fee = result.Result.GlFee, SubscriptionStartDate = result.Result.GlSubscriptionStartDate, SubscriptionEndDate = result.Result.GlNextRenewalDate, BillingCycle = result.Result.GlBillingCycle });
                    }
                }

                if (result.Result.IsPayrollSubscribed)
                {
                    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Payroll", Description = RhyoliteERPConsts.PayrollDescription, ModuleId = RhyoliteERPConsts.Payroll, Status = "Active", StatusCode = 0, Fee = result.Result.PayrollFee, SubscriptionStartDate = result.Result.PayrollSubscriptionStartDate, SubscriptionEndDate = result.Result.PayrollNextRenewalDate, BillingCycle = result.Result.PayrollBillingCycle });
                }
                else
                {
                    if (DateTime.UtcNow > result.Result.PayrollSubscriptionEndDate)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Payroll", Description = RhyoliteERPConsts.PayrollDescription, ModuleId = RhyoliteERPConsts.Payroll, Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.PayrollFee, SubscriptionStartDate = result.Result.PayrollSubscriptionStartDate, SubscriptionEndDate = result.Result.PayrollNextRenewalDate, BillingCycle = result.Result.PayrollBillingCycle });
                    }
                    else if (result.Result.PayrollFee <= 0)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Payroll", Description = RhyoliteERPConsts.PayrollDescription, ModuleId = RhyoliteERPConsts.Payroll, Status = "InActive", StatusCode = 2, Fee = result.Result.PayrollFee, SubscriptionStartDate = result.Result.PayrollSubscriptionStartDate, SubscriptionEndDate = result.Result.PayrollNextRenewalDate, BillingCycle = result.Result.PayrollBillingCycle });
                    }
                }


                if (result.Result.IsSchoolManagerSubscribed)
                {
                    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "School Manager", Description = RhyoliteERPConsts.SchoolManagerDescription, ModuleId = RhyoliteERPConsts.SchoolManager, Status = "Active", StatusCode = 0, Fee = result.Result.SchoolManagerFee, SubscriptionStartDate = result.Result.SchoolManagerSubscriptionStartDate, SubscriptionEndDate = result.Result.SchoolManagerNextRenewalDate, BillingCycle = result.Result.SchoolManagerBillingCycle });
                }
                else
                {
                    if (DateTime.UtcNow > result.Result.SchoolManagerSubscriptionEndDate)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "School Manager", Description = RhyoliteERPConsts.SchoolManagerDescription, ModuleId = RhyoliteERPConsts.SchoolManager, Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.SchoolManagerFee, SubscriptionStartDate = result.Result.SchoolManagerSubscriptionStartDate, SubscriptionEndDate = result.Result.SchoolManagerNextRenewalDate, BillingCycle = result.Result.SchoolManagerBillingCycle });
                    }
                    else if (result.Result.SchoolManagerFee <= 0)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "School Manager", Description = RhyoliteERPConsts.SchoolManagerDescription, ModuleId = RhyoliteERPConsts.SchoolManager, Status = "InActive", StatusCode = 2, Fee = result.Result.SchoolManagerFee, SubscriptionStartDate = result.Result.SchoolManagerSubscriptionStartDate, SubscriptionEndDate = result.Result.SchoolManagerNextRenewalDate, BillingCycle = result.Result.SchoolManagerBillingCycle });
                    }
                }


                if (result.Result.IsPropertyRentalModuleSubscribed)
                {
                    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Property Rental", Description = RhyoliteERPConsts.PropertyRentalModuleDescription, ModuleId = RhyoliteERPConsts.PropertyRentals, Status = "Active", StatusCode = 0, Fee = result.Result.PropertyRentalModuleFee, SubscriptionStartDate = result.Result.PropertyRentalModuleSubscriptionStartDate, SubscriptionEndDate = result.Result.PropertyRentalModuleNextRenewalDate, BillingCycle = result.Result.PropertyRentalModuleBillingCycle });
                }
                else
                {
                    if (DateTime.UtcNow > result.Result.PropertyRentalModuleSubscriptionEndDate)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Property Rental", Description = RhyoliteERPConsts.PropertyRentalModuleDescription, ModuleId = RhyoliteERPConsts.PropertyRentals, Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.PropertyRentalModuleFee, SubscriptionStartDate = result.Result.PropertyRentalModuleSubscriptionStartDate, SubscriptionEndDate = result.Result.PropertyRentalModuleNextRenewalDate, BillingCycle = result.Result.PropertyRentalModuleBillingCycle });
                    }
                    else if (result.Result.SchoolManagerFee <= 0)
                    {
                        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Property Rental", Description = RhyoliteERPConsts.PropertyRentalModuleDescription, ModuleId = RhyoliteERPConsts.PropertyRentals, Status = "InActive", StatusCode = 2, Fee = result.Result.PropertyRentalModuleFee, SubscriptionStartDate = result.Result.PropertyRentalModuleSubscriptionStartDate, SubscriptionEndDate = result.Result.PropertyRentalModuleNextRenewalDate, BillingCycle = result.Result.PropertyRentalModuleBillingCycle });
                    }
                }


                //if (result.Result.IsStockManagerSubscribed)
                //{
                //    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Stock Manager", Status = "Active", StatusCode = 0, Fee = result.Result.StockManagerFee, SubscriptionStartDate = result.Result.StockManagerSubscriptionStartDate, SubscriptionEndDate = result.Result.StockManagerNextRenewalDate, BillingCycle = result.Result.StockManagerBillingCycle });
                //}
                //else
                //{
                //    if (DateTime.UtcNow > result.Result.StockManagerSubscriptionEndDate)
                //    {
                //        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Stock Manager", Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.StockManagerFee, SubscriptionStartDate = result.Result.StockManagerSubscriptionStartDate, SubscriptionEndDate = result.Result.StockManagerNextRenewalDate, BillingCycle = result.Result.StockManagerBillingCycle });
                //    }
                //    else if (result.Result.StockManagerFee <= 0)
                //    {
                //        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "Stock Manager", Status = "InActive", StatusCode = 2, Fee = result.Result.StockManagerFee, SubscriptionStartDate = result.Result.StockManagerSubscriptionStartDate, SubscriptionEndDate = result.Result.StockManagerNextRenewalDate, BillingCycle = result.Result.StockManagerBillingCycle });
                //    }
                //}


                //if (result.Result.IsHrSubscribed)
                //{
                //    subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "HR Manager", Status = "Active", StatusCode = 0, Fee = result.Result.HrFee, SubscriptionStartDate = result.Result.HrSubscriptionStartDate, SubscriptionEndDate = result.Result.HrNextRenewalDate, BillingCycle = result.Result.HrBillingCycle });
                //}
                //else
                //{
                //    if (DateTime.UtcNow > result.Result.HrSubscriptionEndDate)
                //    {
                //        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "HR Manager", Status = "Expired Subscription", StatusCode = 1, Fee = result.Result.HrFee, SubscriptionStartDate = result.Result.HrSubscriptionStartDate, SubscriptionEndDate = result.Result.HrNextRenewalDate, BillingCycle = result.Result.HrBillingCycle });
                //    }
                //    else if (result.Result.HrFee <= 0)
                //    {
                //        subscriptionSummaryDtos.Add(new SubscriptionSummaryDto { ModuleName = "HR Manager", Status = "InActive", StatusCode = 2, Fee = result.Result.HrFee, SubscriptionStartDate = result.Result.HrSubscriptionStartDate, SubscriptionEndDate = result.Result.HrNextRenewalDate, BillingCycle = result.Result.HrBillingCycle });
                //    }
                //}

            }


            return subscriptionSummaryDtos;

        }

        public async Task<object> GetBusinessCategories()
        {
            var url = $"{_configuration["SubscriptionManager:BaseUrl"]}api/services/app/BusinessCategory/ListAll";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);
           
            var request = new RestRequest();

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(80000);

            return await client.GetAsync<object>(request, cancellationTokenSource.Token);

        }

        public async Task<bool> ValidateModuleLicense(int moduleId)
        {

            var url = $"{_configuration["SubscriptionManager:BaseUrl"]}api/services/app/Subscription/ValidateModuleLicense";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);

            var request = new RestRequest()
                            .AddQueryParameter("tenantId", $"{AbpSession.TenantId}")
                            .AddQueryParameter("accountSource", "erp")
                            .AddQueryParameter("moduleId", $"{moduleId}");

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(80000);

            var response = await client.GetAsync<ModuleLicenseInfo>(request, cancellationTokenSource.Token);

            return response != null ? response.Result.IsValid : false;
        }

        public async Task<bool> DebitSmsAccount(SmsBillInfo smsBillInfo)
        {

            var url = $"{_configuration["SubscriptionManager:BaseUrl"]}api/services/app/SmsAccount/TenantSmsBiller";

            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);

            var request = new RestRequest()
                                .AddJsonBody(smsBillInfo);

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(60000);

            var response = await client.PostAsync(request, cancellationTokenSource.Token);

            if (response.IsSuccessful)
            {
                Logger.Info($"sms-billing for tenant: {smsBillInfo.TenantId} at rate: {smsBillInfo.Rate} succeeded.");

            }
            else
            {
                Logger.Info($"sms-billing for tenant: {smsBillInfo.TenantId} at rate: {smsBillInfo.Rate} failed.");

            }

            return response.IsSuccessful;
        }
    }
}
