using Abp.Application.Services;
using RhyoliteERP.Subscription.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Subscription
{
    public interface ISubscriptionAppService: IApplicationService
    {
        Task<BalanceDto> GetSmsAccountBalance();
        Task<List<SubscriptionSummaryDto>> ListSubscribedModules();
        Task<object> ListInvoices(int pageNo, int pageSize);
        Task<object> GetBusinessCategories();
        Task<bool> ValidateModuleLicense(int moduleId);
        Task<bool> DebitSmsAccount(SmsBillInfo smsBillInfo);

    }
}
