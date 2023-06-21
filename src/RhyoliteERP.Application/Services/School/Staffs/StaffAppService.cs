using AutoMapper;
using RhyoliteERP.DomainServices.School.Staffs;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Staffs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Staffs
{
   public class StaffAppService: RhyoliteERPAppServiceBase, IStaffAppService
    {

        private readonly IStaffManager _staffManager;
        private readonly IMapper _mapper;

        public StaffAppService(IStaffManager staffManager, IMapper mapper)
        {
            _staffManager = staffManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll(bool isTeachingStaff)
        {
            return await _staffManager.ListAll(isTeachingStaff);
        }

        public async Task<object> Create(CreateStaffInput input)
        {
            var obj = _mapper.Map<Staff>(input);
            return await _staffManager.Create(obj);
        }

        public async Task Update(UpdateStaffInput input)
        {
            var obj = _mapper.Map<Staff>(input);
            await _staffManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _staffManager.Delete(Id);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _staffManager.GetAsync(id);
        }
    }
}
