using RhyoliteERP.Services.Shared.Countires.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Countires
{
    public interface ICountryAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateCountryInput input);
        Task Update(UpdateCountryInput input);
        Task Delete(Guid Id);

    }
}
