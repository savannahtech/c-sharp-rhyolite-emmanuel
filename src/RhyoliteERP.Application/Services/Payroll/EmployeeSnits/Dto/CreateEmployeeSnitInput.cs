﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeSnits.Dto
{
   public class CreateEmployeeSnitInput
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public decimal SocialSecurityFundEmployerContribution { get; set; }
        public string SocialSecurityNo { get; set; }
        public decimal SocialSecurityFundEmployeeContribution { get; set; }
        public decimal SuperAnnuationEmployerContribution { get; set; }
        public decimal SuperAnnuationEmployeeContribution { get; set; }
        public decimal ProvidentFundEmployeeContribution { get; set; }
        public decimal ProvidentFundEmployerContribution { get; set; }
        public decimal SecondProvidentFundEmployeeContribution { get; set; }
        public decimal SecondProvidentFundEmployerContribution { get; set; }
    }

    public class ImportEmployeeSnitInput
    {
        public string EmployeeIdentifier { get; set; }
        public decimal SocialSecurityFundEmployerContribution { get; set; }
        public string SocialSecurityNo { get; set; }
        public decimal SocialSecurityFundEmployeeContribution { get; set; }
        public decimal SuperAnnuationEmployerContribution { get; set; }
        public decimal SuperAnnuationEmployeeContribution { get; set; }
        public decimal ProvidentFundEmployeeContribution { get; set; }
        public decimal ProvidentFundEmployerContribution { get; set; }
        public decimal SecondProvidentFundEmployeeContribution { get; set; }
        public decimal SecondProvidentFundEmployerContribution { get; set; }
    }
}
