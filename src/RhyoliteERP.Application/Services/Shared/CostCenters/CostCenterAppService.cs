using AutoMapper;
using RhyoliteERP.DomainServices.Shared.CostCenters;
using RhyoliteERP.DomainServices.Shared.Currencies;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.CostCenters.Dto;
using RhyoliteERP.Services.Shared.Currencies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.CostCenters
{
    public class CostCenterAppService: RhyoliteERPAppServiceBase, ICostCenterAppService
    {

        private readonly ICostCenterManager _costCenterManager;
        private readonly IMapper _mapper;

        public CostCenterAppService(ICostCenterManager costCenterManager, IMapper mapper)
        {
            _costCenterManager = costCenterManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _costCenterManager.ListAll();
        }

        public async Task<object> Create(CreateCostCenterInput input)
        {
            var obj = _mapper.Map<CostCenter>(input);
            return await _costCenterManager.Create(obj);
        }

        public async Task Update(UpdateCostCenterInput input)
        {
            var obj = _mapper.Map<CostCenter>(input);
            await _costCenterManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _costCenterManager.Delete(Id);
        }
    }
}
