using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.OvertimeTimeSheets;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.OvertimeTimeSheets.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.OvertimeTimeSheets
{
   public class OvertimeTimeSheetAppService: RhyoliteERPAppServiceBase, IOvertimeTimeSheetAppService
    {
        private readonly IOvertimeTimeSheetManager _overtimeTimeSheetManager;
        private readonly IMapper _mapper;

        public OvertimeTimeSheetAppService(IOvertimeTimeSheetManager overtimeTimeSheetManager, IMapper mapper)
        {
            _overtimeTimeSheetManager = overtimeTimeSheetManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _overtimeTimeSheetManager.ListAll();
        }

        public async Task<object> ListAll(int month, int year)
        {
            return await _overtimeTimeSheetManager.ListAll(month, year);
        }

        public async Task Create(CreateOvertimeTimeSheetInput input)
        {
            var obj = _mapper.Map<OvertimeTimeSheet>(input);
            await _overtimeTimeSheetManager.Create(obj);
        }

        public async Task Update(UpdateOvertimeTimeSheetInput input)
        {
            var obj = _mapper.Map<OvertimeTimeSheet>(input);
            await _overtimeTimeSheetManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _overtimeTimeSheetManager.Delete(Id);

        }
    }
}
