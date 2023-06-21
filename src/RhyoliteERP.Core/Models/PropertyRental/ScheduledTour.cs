﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class ScheduledTour: Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string ScheduleType { get; set; } // In Person, Video Chat
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public Guid PropertyUnitId { get; set; }
        public string PropertyUnitNo { get; set; }
        public int TenantId { get; set; }

    }
}
