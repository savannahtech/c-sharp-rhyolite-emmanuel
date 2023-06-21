using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Parents
{
   public class ParentManager: Abp.Domain.Services.DomainService, IParentManager
    {
        private readonly IRepository<Parent, Guid> _repositoryParent;
        private readonly IRepository<Relationship, Guid> _repositoryRelationship;
        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IRepository<StudentParent, Guid> _repositoryStudentParent;
        public ParentManager(IRepository<Parent, Guid> repositoryParent, IRepository<Relationship, Guid> repositoryRelationship, IRepository<Student, Guid> repositoryStudent, IRepository<StudentParent, Guid> repositoryStudentParent)
        {
            _repositoryParent = repositoryParent;
            _repositoryRelationship = repositoryRelationship;
            _repositoryStudent = repositoryStudent;
            _repositoryStudentParent = repositoryStudentParent;
        }

        public async Task<object> Create(Parent entity)
        {
            var firstGuardianRelationship = await _repositoryRelationship.FirstOrDefaultAsync(x => x.Id == entity.FirstGuardianRelationshipId);
            var SecondGuardianRelationship = await _repositoryRelationship.FirstOrDefaultAsync(x => x.Id == entity.SecondGuardianRelationshipId);

            if (firstGuardianRelationship != null)
            {
                entity.FirstGuardianRelationshipName = firstGuardianRelationship.Name;
            }

            if (SecondGuardianRelationship != null)
            {
                entity.SecondGuardianRelationshipName = SecondGuardianRelationship.Name;
            }

            if (!string.IsNullOrEmpty(entity.FirstGuardianPhoneNo))
            {
                entity.FirstGuardianPhoneNo = Regex.Replace(entity.FirstGuardianPhoneNo, @"\s", "").Trim();
            }

            if (!string.IsNullOrEmpty(entity.SecondGuardianPhoneNo))
            {
                entity.SecondGuardianPhoneNo = Regex.Replace(entity.SecondGuardianPhoneNo, @"\s", "").Trim();

            }
            return await _repositoryParent.InsertAsync(entity);

        }

        public async Task<IEnumerable<object>> ListParentWards()
        {
            var students = await _repositoryStudent.GetAllListAsync();
            var stuParents = await _repositoryStudentParent.GetAllListAsync();
             
            var query = from u1 in students
                        join u2 in stuParents on u1.Id equals u2.StudentId
                        
                        select new
                        {
                            u1.StudentIdentifier,
                            StudentName = u1.MiddleName == null ? $"{u1.LastName}, {u1.FirstName}" : $"{u1.LastName}, {u1.FirstName} {u1.MiddleName}",
                            Id = u2.ParentId,
                            u2.StudentId,
                            u1.ClassName,
                        };

            return query.ToList();
        }
        public async Task<object> GetParentInfo(Guid id)
        {
            return await _repositoryParent.FirstOrDefaultAsync(a => a.Id == id);
        }



        public async Task<IEnumerable<object>> ListAll(Guid id)
        {
            return await _repositoryParent.GetAllListAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryParent.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositoryParent.DeleteAsync(id);
        }

        public async Task<IEnumerable<object>> ListParents()
        {
            var datta = await _repositoryParent.GetAllListAsync();
            return (from u1 in datta
                    select new
                    {
                        u1.Id,
                        ParentName = u1.FirstGuardianName + "-" + u1.FirstGuardianPhoneNo + ";" + u1.SecondGuardianName + "-" + u1.SecondGuardianPhoneNo

                    }).ToList();
        }

        public async Task<object> Update(Parent entity)
        {
            var firstGuardianRelationship = await _repositoryRelationship.FirstOrDefaultAsync(x => x.Id == entity.FirstGuardianRelationshipId);
            var SecondGuardianRelationship = await _repositoryRelationship.FirstOrDefaultAsync(x => x.Id == entity.SecondGuardianRelationshipId);

            if (firstGuardianRelationship != null)
            {
                entity.FirstGuardianRelationshipName = firstGuardianRelationship.Name;
            }

            if (SecondGuardianRelationship != null)
            {
                entity.SecondGuardianRelationshipName = SecondGuardianRelationship.Name;
            }

            if (!string.IsNullOrEmpty(entity.FirstGuardianPhoneNo))
            {
                entity.FirstGuardianPhoneNo = Regex.Replace(entity.FirstGuardianPhoneNo, @"\s", "").Trim();
            }

            if (!string.IsNullOrEmpty(entity.SecondGuardianPhoneNo))
            {
                entity.SecondGuardianPhoneNo = Regex.Replace(entity.SecondGuardianPhoneNo, @"\s", "").Trim();

            }
            return await _repositoryParent.UpdateAsync(entity);
        }

    }
}
