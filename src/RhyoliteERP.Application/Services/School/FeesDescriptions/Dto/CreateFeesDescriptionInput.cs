using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.FeesDescriptions.Dto
{
   public class CreateFeesDescriptionInput
    {
        public string Description { get; set; }
        public Guid BillTypeId { get; set; }
    }
}
