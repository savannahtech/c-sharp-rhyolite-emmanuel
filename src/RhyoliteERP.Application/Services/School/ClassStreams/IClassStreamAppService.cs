using Abp.Application.Services;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.ClassStreams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ClassStreams
{
   public interface IClassStreamAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateClassStreamInput input);
        Task Update(UpdateClassStreamInput input);
        Task Delete(Guid Id);


    }
}
