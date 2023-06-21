using RhyoliteERP.Services.Shared.CountryStates.Dto;
using RhyoliteERP.Services.Shared.Cties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.CountryStates
{
    public interface ICountryStateAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateCountryStateInput input);
        Task Update(UpdateCountryStateInput input);
        Task Delete(Guid Id);
    }
}
