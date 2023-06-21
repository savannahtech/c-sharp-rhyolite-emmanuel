using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyTasks.Dto
{
    public class UpdatePropertyTaskInput : EntityDto<Guid>
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string Name { get; set; }
        public string UnitNo { get; set; }
        public Guid UnitId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string AssignedTo { get; set; }
        public Guid ResidentId { get; set; }
        public string ResidentName { get; set; }
        public Guid RentalOwnerId { get; set; }
        public string RentalOwnerName { get; set; }
        public List<string> Attachments { get; set; }
        public int TenantId { get; set; }
    }
}
