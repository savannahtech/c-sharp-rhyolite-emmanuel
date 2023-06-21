using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Countries
{
   public interface ICountryManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(Country input);
        Task<Country> GetNationality(string nationality);
        Task Update(Country input);
        Task Delete(Guid Id);
    }
}
