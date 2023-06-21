using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.TaxTables;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.TaxTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.TaxTables
{
   public class TaxTableAppService: RhyoliteERPAppServiceBase, ITaxTableAppService
    {
        private readonly ITaxTableManager _taxTableManager;
        private readonly IMapper _mapper;

        public TaxTableAppService(ITaxTableManager taxTableManager, IMapper mapper)
        {
            _taxTableManager = taxTableManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _taxTableManager.ListAll();
        }

        public async Task Create(CreateTaxTableInput input)
        {
            var obj = _mapper.Map<TaxTable>(input);
            await _taxTableManager.Create(obj);
        }

        public async Task Update(UpdateTaxTableInput input)
        {
            var obj = _mapper.Map<TaxTable>(input);
            await _taxTableManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _taxTableManager.Delete(Id);
        }
    }
}
