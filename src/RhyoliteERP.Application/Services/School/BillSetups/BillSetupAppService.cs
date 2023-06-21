using AutoMapper;
using RhyoliteERP.DomainServices.School.BillSetups;
using RhyoliteERP.DomainServices.School.BillTypes;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.BillSetups.Dto;
using RhyoliteERP.Services.School.BillTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillSetups
{
   public class BillSetupAppService: RhyoliteERPAppServiceBase, IBillSetupAppService
    {

        private readonly IBillSetupManager _billSetupManager;
        private readonly IMapper _mapper;

        public BillSetupAppService(IBillSetupManager billSetupManager, IMapper mapper)
        {
            _billSetupManager = billSetupManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _billSetupManager.ListAll(academicYearId, termId, classId, billTypeId);
        }

        public async Task<object> GetAsync(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _billSetupManager.GetAsync(academicYearId, termId, classId, billTypeId);
        }

        public async Task<object> Create(CreateBillSetupInput input)
        {
            var obj = _mapper.Map<BillSetup>(input);
            return await _billSetupManager.Create(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _billSetupManager.Delete(Id);

        }

        public async Task DeleteDetail(Guid Id, Guid billDetailId)
        {
            await _billSetupManager.DeleteDetail(Id, billDetailId);

        }
    }
}
