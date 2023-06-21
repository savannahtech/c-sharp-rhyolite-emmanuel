﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared
{
   public interface IFileManager: Abp.Domain.Services.IDomainService
    {
        Task<byte[]> DownloadFileAsBytes(string url);
    }
}
