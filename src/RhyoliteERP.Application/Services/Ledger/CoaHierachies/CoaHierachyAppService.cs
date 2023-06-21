using AutoMapper;
using RhyoliteERP.DomainServices.Ledger.CoaHierachies;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Services.Ledger.CoaHierachies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.CoaHierachies
{
   public class CoaHierachyAppService: RhyoliteERPAppServiceBase, ICoaHierachyAppService
    {
        private readonly ICoaHierachyManager _coaHierachyManager;
        private readonly IMapper _mapper;

        public CoaHierachyAppService(ICoaHierachyManager coaHierachyManager, IMapper mapper)
        {
            _coaHierachyManager = coaHierachyManager;
            _mapper = mapper;
        }


        public async Task<object> Create(CreateCoaHierachyInput input)
        {
            var obj = _mapper.Map<CoaHierachy>(input);
           return await _coaHierachyManager.Create(obj);
        }

        public async Task Update(UpdateCoaHierachyInput input)
        {
            var obj = _mapper.Map<CoaHierachy>(input);
            await _coaHierachyManager.Update(obj);
        }

        public async Task<object> ListAll()
        {
           return await _coaHierachyManager.ListAll();
        }

        public async Task Delete(Guid id)
        {
            await _coaHierachyManager.Delete(id);
        }
    }
}
