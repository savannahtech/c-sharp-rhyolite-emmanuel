using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeaseApplicants.Dto
{
    public class CreateLeaseApplicantInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public Guid PropertyUnitId { get; set; }
        public string PropertyUnitName { get; set; }
    }
}
