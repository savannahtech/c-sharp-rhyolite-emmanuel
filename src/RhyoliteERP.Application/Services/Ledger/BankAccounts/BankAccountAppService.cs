using AutoMapper;
using RhyoliteERP.DomainServices.Ledger.BankAccounts;
using RhyoliteERP.DomainServices.Ledger.ImprestCategories;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Services.Ledger.BankAccounts.Dto;
using RhyoliteERP.Services.Ledger.ImprestCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.BankAccounts
{
    public class BankAccountAppService: RhyoliteERPAppServiceBase, IBankAccountAppService
    {

        private readonly IBankAccountManager _bankAccountManager;
        private readonly IMapper _mapper;

        public BankAccountAppService(IBankAccountManager bankAccountManager, IMapper mapper)
        {
            _bankAccountManager = bankAccountManager;
            _mapper = mapper;
        }


        public async Task<object> Create(CreateBankAccountInput input)
        {
            var obj = _mapper.Map<BankAccount>(input);
            return await _bankAccountManager.Create(obj);
        }

        public async Task Update(UpdateBankAccountInput input)
        {
            var obj = _mapper.Map<BankAccount>(input);
            await _bankAccountManager.Update(obj);
        }

        public async Task<object> ListAll()
        {
            return await _bankAccountManager.ListAll();
        }

        public async Task Delete(Guid id)
        {
            await _bankAccountManager.Delete(id);
        }
    }
}
