using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentParents
{
   public class StudentParentManager: Abp.Domain.Services.DomainService, IStudentParentManager
    {
        private readonly IRepository<StudentParent, Guid> _repositoryStudentParent;
        private readonly IRepository<Parent, Guid> _repositoryParent;

        public StudentParentManager(IRepository<StudentParent, Guid> repositoryStudentParent, IRepository<Parent, Guid> repositoryParent)
        {
            _repositoryStudentParent = repositoryStudentParent;
            _repositoryParent = repositoryParent;
        }

        public async Task Create(StudentParent entity)
        {
            var datta = await _repositoryStudentParent.FirstOrDefaultAsync(x => x.StudentId == entity.StudentId);
            if (datta == null)
            {
                await _repositoryStudentParent.InsertAsync(entity);
            }
            else
            {
                datta.ParentId = entity.ParentId;

                await _repositoryStudentParent.UpdateAsync(datta);

            }

        }


        public async Task<object> GetStudentParent(Guid studentId)
        {
            var studParentObj = await _repositoryStudentParent.FirstOrDefaultAsync(a => a.StudentId == studentId);
            if (studParentObj != null)
            {
                return await _repositoryParent.GetAsync(studParentObj.ParentId);
                //return await _repositoryStudentParent.FirstOrDefaultAsync(x => x.Id == studParentObj.ParentId);
            }
            else
            {
                return new { Message = "No Guardian Found." };
            }

        }

        public async Task Delete(Guid id)
        {
            await _repositoryStudentParent.DeleteAsync(id);
        }

        public async Task Update(StudentParent entity)
        {
            await _repositoryStudentParent.UpdateAsync(entity);
        }


        public async Task<IEnumerable<Guid>> ListChildren(Guid parentId)
        {
            var datta = await _repositoryStudentParent.GetAllListAsync(x => x.ParentId == parentId);
            return datta.Select(b => b.StudentId).ToList();
        }
    }
}
