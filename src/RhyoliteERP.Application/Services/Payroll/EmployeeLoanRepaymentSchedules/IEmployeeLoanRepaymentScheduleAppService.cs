﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeLoanRepaymentSchedules
{
    public interface IEmployeeLoanRepaymentScheduleAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();

    }
}
