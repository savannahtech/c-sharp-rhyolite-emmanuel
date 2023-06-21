using Abp.Application.Services;
using RhyoliteERP.DomainServices.Shared.ReportDownloads.Dto;
using RhyoliteERP.Services.Shared.ReportDownloads.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.ReportDownloads
{
    public interface IReportDownloadAppService: IApplicationService
    {
        Task Create(CreateReportDownloadInput input);

        Task Update(Guid reportId, string filePath);

        Task<PaginatedDownload> ListAll(int pageNo, int pageSize);

    }
}
