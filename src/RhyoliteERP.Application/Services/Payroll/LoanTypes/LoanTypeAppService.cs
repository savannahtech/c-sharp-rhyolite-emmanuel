using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.LoanTypes;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.LoanTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.LoanTypes
{
   public class LoanTypeAppService : RhyoliteERPAppServiceBase, ILoanTypeAppService
    {
        private readonly ILoanTypeManager _loanTypeManager;
        private readonly IMapper _mapper;

        public LoanTypeAppService(ILoanTypeManager loanTypeManager, IMapper mapper)
        {
            _loanTypeManager = loanTypeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _loanTypeManager.ListAll();
        }

        public async Task<object> Create(CreateLoanTypeInput input)
        {
            var obj = _mapper.Map<LoanType>(input);
            return await _loanTypeManager.Create(obj);
        }

        public async Task Update(UpdateLoanTypeInput input)
        {
            var obj = _mapper.Map<LoanType>(input);
            await _loanTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _loanTypeManager.Delete(Id);

        }
    }
}
