using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.FileTransfer.Dto
{
    public class FileTransferDto
    {
        public IFormFile dataFile { get; set; }
        public string fileExtension { get; set; }
    }
}
