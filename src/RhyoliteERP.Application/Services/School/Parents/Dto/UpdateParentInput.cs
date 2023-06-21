using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Parents.Dto
{
   public class UpdateParentInput: EntityDto<Guid>
    {
        public string FirstGuardianName { get; set; }
        public string FirstGuardianPhoneNo { get; set; }
        public string FirstGuardianEmail { get; set; }
        public string FirstGuardianProfession { get; set; }
        public Guid FirstGuardianRelationshipId { get; set; }
        public string FirstGuardianRelationshipName { get; set; }
        //father...
        public string SecondGuardianProfession { get; set; }
        public string SecondGuardianEmail { get; set; }
        public string SecondGuardianPhoneNo { get; set; }
        public string SecondGuardianName { get; set; }
        public Guid SecondGuardianRelationshipId { get; set; }
        public string SecondGuardianRelationshipName { get; set; }

        public int TenantId { get; set; }
    }
}
