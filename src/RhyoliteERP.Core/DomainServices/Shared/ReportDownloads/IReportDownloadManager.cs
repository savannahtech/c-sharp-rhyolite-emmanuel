﻿using RhyoliteERP.DomainServices.Shared.ReportDownloads.Dto;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.ReportDownloads
{
    public interface IReportDownloadManager : Abp.Domain.Services.IDomainService
    {
        Task Create(ReportDownload input);
        Task Update(Guid reportId, string filePath);
        Task<PaginatedDownload> ListAll(int pageNo, int pageSize);
    }
}
