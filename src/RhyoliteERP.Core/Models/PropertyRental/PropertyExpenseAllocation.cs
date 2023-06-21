using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class PropertyExpenseAllocation : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "jsonb")] public List<ExpenseAllocation> Details { get; set; }
        public int TenantId { get; set; }

    }
}
