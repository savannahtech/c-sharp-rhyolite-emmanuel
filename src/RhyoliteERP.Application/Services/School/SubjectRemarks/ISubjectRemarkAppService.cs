using Abp.Application.Services;
using RhyoliteERP.Services.School.SubjectRemarks.Dto;
using RhyoliteERP.Services.School.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SubjectRemarks
{
    public interface ISubjectRemarkAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(CreateSubjectRemarkInput input);
        Task Update(UpdateSubjectRemarkInput input);
        Task Delete(Guid Id);
    }
}
