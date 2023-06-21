using AutoMapper;
using RhyoliteERP.DomainServices.Ledger.CoaHierachies;
using RhyoliteERP.DomainServices.Ledger.ImprestCategories;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Services.Ledger.CoaHierachies.Dto;
using RhyoliteERP.Services.Ledger.ImprestCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.ImprestCategories
{
    public class ImprestCategoryAppService: RhyoliteERPAppServiceBase, IImprestCategoryAppService
    {

        private readonly IImprestCategoryManager _imprestCategoryManager;
        private readonly IMapper _mapper;

        public ImprestCategoryAppService(IImprestCategoryManager imprestCategoryManager, IMapper mapper)
        {
            _imprestCategoryManager = imprestCategoryManager;
            _mapper = mapper;
        }


        public async Task<object> Create(CreateImprestCategoryInput input)
        {
            var obj = _mapper.Map<ImprestCategory>(input);
            return await _imprestCategoryManager.Create(obj);
        }

        public async Task Update(UpdateImprestCategoryInput input)
        {
            var obj = _mapper.Map<ImprestCategory>(input);
            await _imprestCategoryManager.Update(obj);
        }

        public async Task<object> ListAll()
        {
            return await _imprestCategoryManager.ListAll();
        }

        public async Task Delete(Guid id)
        {
            await _imprestCategoryManager.Delete(id);
        }

    }
}
