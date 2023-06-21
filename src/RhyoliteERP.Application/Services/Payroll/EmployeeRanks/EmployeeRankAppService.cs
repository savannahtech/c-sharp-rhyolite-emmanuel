using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeRanks;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeRanks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeRanks
{
   public class EmployeeRankAppService: RhyoliteERPAppServiceBase, IEmployeeRankAppService
    {
        private readonly IEmployeeRankManager _employeeRankManager;
        private readonly IMapper _mapper;

        public EmployeeRankAppService(IEmployeeRankManager employeeRankManager, IMapper mapper)
        {
            _employeeRankManager = employeeRankManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeRankManager.ListAll();
        }

        public async Task<object> Create(CreateEmployeeRankInput input)
        {
            var obj = _mapper.Map<EmployeeRank>(input);
            return await _employeeRankManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeRankInput input)
        {
            var obj = _mapper.Map<EmployeeRank>(input);
            await _employeeRankManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeRankManager.Delete(Id);

        }
    }
}
