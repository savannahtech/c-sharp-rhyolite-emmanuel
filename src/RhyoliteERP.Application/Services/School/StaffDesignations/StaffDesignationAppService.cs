using AutoMapper;
using RhyoliteERP.DomainServices.School.StaffDesignations;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.StaffDesignations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StaffDesignations
{
   public class StaffDesignationAppService: RhyoliteERPAppServiceBase, IStaffDesignationAppService
    {
        private readonly IStaffDesignationManager _staffDesignationManager;
        private readonly IMapper _mapper;

        public StaffDesignationAppService(IStaffDesignationManager staffDesignationManager, IMapper mapper)
        {
            _staffDesignationManager = staffDesignationManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _staffDesignationManager.ListAll();
        }

        public async Task<object> Create(CreateStaffDesignationInput input)
        {
            var obj = _mapper.Map<StaffDesignation>(input);
            return await _staffDesignationManager.Create(obj);
        }

        public async Task Update(UpdateStaffDesignationInput input)
        {
            var obj = _mapper.Map<StaffDesignation>(input);
            await _staffDesignationManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _staffDesignationManager.Delete(Id);

        }
    }
}
