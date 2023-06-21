using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.ReportDownloads.Dto
{
    public class PaginatedDownload
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageNo { get; set; }
        public List<ReportDownload> Downloads { get; set; }
    }
}
