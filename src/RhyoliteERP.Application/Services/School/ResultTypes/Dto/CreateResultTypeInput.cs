using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ResultTypes.Dto
{
   public class CreateResultTypeInput
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public Guid LevelId { get; set; }
    }
}
