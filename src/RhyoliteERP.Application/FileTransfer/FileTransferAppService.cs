using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhyoliteERP.FileTransfer.Dto;

namespace RhyoliteERP.FileTransfer
{
    public class FileTransferAppService: RhyoliteERPAppServiceBase, IFileTransferAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _siteBaseUrl;

        public FileTransferAppService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _siteBaseUrl = configuration["SiteBaseUrl"];

        }

        [HttpPost]
        public async Task<string> UploadFile([FromForm] FileTransferDto fileTransferDto)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var wwwRoot = _webHostEnvironment.WebRootPath;

            var fileName = Guid.NewGuid().ToString("N");

            
            var filepath = $"ReportDownload/{fileName}.{fileTransferDto.fileExtension}";

            await using var fileStream = new FileStream(Path.Combine(wwwRoot + $"/ReportDownload/{fileName}.{fileTransferDto.fileExtension}"), FileMode.Create, FileAccess.Write);
            await fileTransferDto.dataFile.CopyToAsync(fileStream);

            Logger.Info($"File Upload Execution Time: {watch.ElapsedMilliseconds} ms");

            return $"{_siteBaseUrl}{filepath}.{fileTransferDto.fileExtension}";
        }
    }
}
