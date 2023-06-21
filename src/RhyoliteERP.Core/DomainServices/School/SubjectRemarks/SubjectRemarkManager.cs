using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SubjectRemarks
{
   public class SubjectRemarkManager:Abp.Domain.Services.DomainService, ISubjectRemarkManager
    {
        private readonly IRepository<SubjectRemark, Guid> _repositorySubjectRemark;

        public SubjectRemarkManager(IRepository<SubjectRemark, Guid> repositorySubjectRemark)
        {
            _repositorySubjectRemark = repositorySubjectRemark;
        }

        public async Task Create(SubjectRemark entity)
        {
           await _repositorySubjectRemark.InsertAsync(entity);

        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositorySubjectRemark.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositorySubjectRemark.DeleteAsync(id);
        }

        public async Task Update(SubjectRemark entity)
        {
            await _repositorySubjectRemark.UpdateAsync(entity);
        }
    }
}
