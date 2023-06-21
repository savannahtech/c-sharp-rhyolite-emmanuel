using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.FileTransfer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.FileTransfer
{
    public interface IFileTransferAppService: Abp.Application.Services.IApplicationService
    {
        Task<string> UploadFile([FromForm] FileTransferDto fileTransferDto);

    }
}
