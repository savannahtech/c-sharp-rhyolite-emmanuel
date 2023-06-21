using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.AllowanceTypes;
using RhyoliteERP.DomainServices.Payroll.AllowanceTypes.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.AllowanceTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.AllowanceTypes
{
   public class AllowanceTypeAppService : RhyoliteERPAppServiceBase, IAllowanceTypeAppService
    {
        private readonly IAllowanceTypeManager _allowanceTypeManager;
        private readonly IMapper _mapper;

        public AllowanceTypeAppService(IAllowanceTypeManager allowanceTypeManager, IMapper mapper)
        {
            _allowanceTypeManager = allowanceTypeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _allowanceTypeManager.ListAll();
        }

        public async Task<object> GetByCategory(Guid categoryId)
        {
            return await _allowanceTypeManager.ListAll(categoryId);
        }

        public async Task<object> ListAll(Guid allowanceTypeId, Guid employeeId, Guid categoryId)
        {
            return await _allowanceTypeManager.ListAll(allowanceTypeId, employeeId, categoryId);
        }

        public async Task<object> Create(CreateAllowanceTypeInput input)
        {
            var obj = _mapper.Map<AllowanceType>(input);
            return await _allowanceTypeManager.Create(obj);
        }

        public async Task Update(UpdateAllowanceTypeInput input)
        {
            var obj = _mapper.Map<AllowanceType>(input);
            await _allowanceTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _allowanceTypeManager.Delete(Id);            
        }

        public async Task<object> CreateAllowanceRate(AllowanceRateInput input)
        {
            return await _allowanceTypeManager.CreateAllowanceRate(input);
        }

        public async Task UpdateAllowanceRate(AllowanceRateInput input)
        {
            await _allowanceTypeManager.UpdateAllowanceRate(input);
        }

        public async Task DeleteAllowanceRate(AllowanceRateInput input)
        {
            await _allowanceTypeManager.DeleteAllowanceRate(input);
        }
    }
}
