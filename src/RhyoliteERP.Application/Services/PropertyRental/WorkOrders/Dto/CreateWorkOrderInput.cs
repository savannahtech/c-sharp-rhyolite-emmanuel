using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.WorkOrders.Dto
{
    public class CreateWorkOrderInput
    {
        public string Subject { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string Category { get; set; }
        public Guid VendorId { get; set; }
        public string VendorName { get; set; }
        public string InvoiceNo { get; set; }
        public string WorkToBeDone { get; set; }
        public string VendorNotes { get; set; }
        public long AssignedToUserId { get; set; }
        public string AssignedToUserName { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public List<string> AttachmentFiles { get; set; }
    }
}
