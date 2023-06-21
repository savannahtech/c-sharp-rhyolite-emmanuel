using AutoMapper;
using RhyoliteERP.DomainServices.Ledger.CoaDetails;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Services.Ledger.CoaDetails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.CoaDetails
{
   public class CoaDetailAppService: RhyoliteERPAppServiceBase, ICoaDetailAppService
    {
        private readonly ICoaDetailManager _coaDetailManager;
        private readonly IMapper _mapper;

        public CoaDetailAppService(ICoaDetailManager coaControlManager, IMapper mapper)
        {
            _coaDetailManager = coaControlManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _coaDetailManager.ListAll();
        }

        public async Task<object> ListActiveAccounts()
        {
            return await _coaDetailManager.ListActiveAccounts();
        }

        public async Task<object> Create(CreateCoaDetailInput input)
        {
            var obj = _mapper.Map<CoaDetail>(input);
            return await _coaDetailManager.Create(obj);
        }

        public async Task Update(UpdateCoaDetailInput input)
        {
            var obj = _mapper.Map<CoaDetail>(input);
            await _coaDetailManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _coaDetailManager.Delete(Id);
        }
    }
}
