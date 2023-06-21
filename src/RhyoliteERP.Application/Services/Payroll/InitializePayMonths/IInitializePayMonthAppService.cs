using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.InitializePayMonths.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.InitializePayMonths
{
    public interface IInitializePayMonthAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> GetData();
        Task Create(CreateInitializePayMonthInput entity);
    }
}
