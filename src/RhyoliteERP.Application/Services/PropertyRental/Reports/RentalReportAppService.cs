using RhyoliteERP.DomainServices.PropertyRental.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Reports
{
    public class RentalReportAppService: RhyoliteERPAppServiceBase, IRentalReportAppService
    {
        private readonly IRentalReportManager _rentalReportManager;

        public RentalReportAppService(IRentalReportManager rentalReportManager)
        {
            _rentalReportManager = rentalReportManager;
        }

        public async Task<object> GetBusinessProfile(string title)
        {
            return await _rentalReportManager.GetBusinessProfile(title);
        }


    }
}
