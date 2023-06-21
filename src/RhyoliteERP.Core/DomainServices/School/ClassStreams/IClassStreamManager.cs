using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ClassStreams
{
   public interface IClassStreamManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(ClassStream input);
        Task Update(ClassStream input);
        Task Delete(Guid Id);
    }
}
