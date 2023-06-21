using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Authorization
{
    public interface IValidateTenant : Abp.Application.Services.IApplicationService
    {
        Task<int> Validate();

    }
}
