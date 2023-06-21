using RhyoliteERP.DomainServices.School.StudentStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentStatements
{
    public class StudentStatementAppService: RhyoliteERPAppServiceBase, IStudentStatementAppService
    {
        public readonly IStudentStatementManager _studentStatementManager;

        public StudentStatementAppService(IStudentStatementManager studentStatementManager)
        {
            _studentStatementManager = studentStatementManager;
        }

        public async Task<object> GetStatement(Guid studentId)
        {
            return await _studentStatementManager.GetStatement(studentId);
        }
    }
}
