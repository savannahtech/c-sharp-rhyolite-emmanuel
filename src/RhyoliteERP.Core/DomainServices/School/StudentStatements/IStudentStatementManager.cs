using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentStatements
{
    public interface IStudentStatementManager: Abp.Domain.Services.IDomainService
    {
        Task<object> GetStatement(Guid studentId);
    }
}
