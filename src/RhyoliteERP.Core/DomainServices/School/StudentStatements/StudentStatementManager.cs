using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentStatements
{
    public class StudentStatementManager: Abp.Domain.Services.DomainService, IStudentStatementManager
    {
        private readonly IRepository<StudentStatement, Guid> _repositoryStudentStatement;

        public StudentStatementManager(IRepository<StudentStatement, Guid> repositoryStudentStatement)
        {
            _repositoryStudentStatement = repositoryStudentStatement;
        }

        public async Task<object> GetStatement(Guid studentId)
        {
            return await _repositoryStudentStatement.FirstOrDefaultAsync(x => x.StudentId == studentId);
        }

    }
}
