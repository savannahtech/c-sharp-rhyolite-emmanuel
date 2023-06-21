using Abp.Application.Services;
using RhyoliteERP.Services.Ledger.BankAccounts.Dto;
using RhyoliteERP.Services.Ledger.ImprestCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.BankAccounts
{
    public interface IBankAccountAppService: IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateBankAccountInput input);
        Task Update(UpdateBankAccountInput input);
        Task Delete(Guid Id);

    }
}
