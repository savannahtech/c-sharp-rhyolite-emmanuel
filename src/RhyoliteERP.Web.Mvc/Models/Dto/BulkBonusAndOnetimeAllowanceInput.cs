using RhyoliteERP.Services.Payroll.BonusAndOnetimeAllowances.Dto;
using RhyoliteERP.Services.Payroll.EmployeeOnetimeDeductions.Dto;
using System;
using System.Collections.Generic;

namespace RhyoliteERP.Web.Models.Dto
{
    public class BulkBonusAndOnetimeAllowanceInput
    {
        public List<CreateBonusAndOnetimeAllowanceInput> BonusList { get; set; }
    }

    public class BulkOnetimeDeductionInput
    {
        public List<CreateEmployeeOnetimeDeductionInput> DeductionList { get; set; }
    }

    public class LoanApprovalDto
    {
        public List<Guid> Ids { get; set; }
        public string ApprovalType { get; set; }
    }
}
