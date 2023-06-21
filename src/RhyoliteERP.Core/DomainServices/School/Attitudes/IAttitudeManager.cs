using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Attitudes
{
   public interface IAttitudeManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(Attitude input);
        Task Update(Attitude input);
        Task Delete(Guid Id);
    }
}
