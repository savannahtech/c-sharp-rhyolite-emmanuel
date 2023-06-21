using AutoMapper;
using RhyoliteERP.DomainServices.School.BillTypes;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.BillTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillTypes
{
   public class BillTypeAppService : RhyoliteERPAppServiceBase, IBillTypeAppService
    {
        private readonly IBillTypeManager _billTypeManager;
        private readonly IMapper _mapper;

        public BillTypeAppService(IBillTypeManager billTypeManager, IMapper mapper)
        {
            _billTypeManager = billTypeManager;
            _mapper = mapper;
        }


        public async Task<object> ListAll()
        {
            return await _billTypeManager.ListAll();
        }

        public async Task<object> Create(CreateBillTypeInput input)
        {
            var obj = _mapper.Map<BillType>(input);
            return await _billTypeManager.Create(obj);
        }

        public async Task Update(UpdateBillTypeInput input)
        {
            var obj = _mapper.Map<BillType>(input);
            await _billTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _billTypeManager.Delete(Id);

        }

    }
}
