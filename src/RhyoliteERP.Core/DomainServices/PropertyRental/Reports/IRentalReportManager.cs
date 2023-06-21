using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Reports
{
    public interface IRentalReportManager: Abp.Domain.Services.IDomainService
    {
        Task<object> GetBusinessProfile(string title);
    }
}
