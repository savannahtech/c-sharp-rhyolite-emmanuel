using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Reports
{
    public interface IRentalReportAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> GetBusinessProfile(string title);

    }
}
