using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class Lease : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public Guid PropertyUnitId { get; set; }
        public string PropertyUnitName { get; set; }
        public bool IsSigned { get; set; }
        public string LeaseType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "jsonb")] public List<TenantOrCosigner> TenantOrCosigners { get; set; }
        [Column(TypeName = "jsonb")] public List<RentCharge> RentCharges { get; set; }
        public string RentCycle { get; set; }
        public decimal TotalAmount { get; set; }

        //security deposit ...
        public decimal SecurityDepositAmount { get; set; }
        public DateTime SecurityDepositDueDate { get; set; }
        
        //charges....
        public Guid ChargeAccountId { get; set; }
        public DateTime ChargeNextDueDate { get; set; }
        public decimal ChargeAmount { get; set; }
        public string ChargeFrequency { get; set; }
        public string ChargeMemo { get; set; }
        [Column(TypeName = "jsonb")] public List<string> Files { get; set; }
        public int TenantId { get; set; }

    }
}
