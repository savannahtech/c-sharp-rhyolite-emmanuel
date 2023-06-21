using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Banking
{
    public class Client : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string ExternalId { get; set; }
        public int Status { get; set; }
        public int SubStatus { get; set; }
        public DateTime OfficeJoiningDate { get; set; }
        public Guid OfficeId { get; set; }
        public Guid TransferToOfficeId { get; set; }
        public string PrimaryContactPerson { get; set; }
        public string SecondaryContactPerson { get; set; }
        public string PrimaryContactNo { get; set; }
        public string SecondaryContactNo { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public string ActivatedBy { get; set; }
        public int ActivatedByUserId { get; set; }
        public string ActivationReason { get; set; }
        [Column(TypeName = "jsonb")] public List<string> ActivationSupportingDocuments { get; set; }
        public DateTime? WithDrawnOn { get; set; }
        public string WithDrawnBy { get; set; }
        public int WithDrawnByUserId { get; set; }
        public string WithDrawnReason { get; set; }
        [Column(TypeName = "jsonb")] public List<string> WithDrawnSupportingDocuments { get; set; }
        public DateTime? ReactivatedOn { get; set; }
        public string ReactivatedBy { get; set; }
        public int ReactivatedUserId { get; set; }
        public string ReactivationReason { get; set; }
        [Column(TypeName = "jsonb")] public List<string> ReactivationSupportingDocuments { get; set; }
        public Guid DefaultSavingsProductId { get; set; }
        public string DefaultSavingsAccount { get; set; }
        public int TenantId { get; set; }

    }
}
