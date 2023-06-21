using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using RhyoliteERP.Authorization;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentStatuses
{
   public class StudentStatusManager: Abp.Domain.Services.DomainService, IStudentStatusManager
    {
        private readonly IRepository<StudentStatus, Guid> _repositoryStudentStatus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public StudentStatusManager(IRepository<StudentStatus, Guid> repositoryStudentStatus, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositoryStudentStatus = repositoryStudentStatus;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<object> Create(StudentStatus entity)
        {
            var datta = await _repositoryStudentStatus.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                var isdefaultRow = await _repositoryStudentStatus.FirstOrDefaultAsync(x => x.IsDefault == entity.IsDefault);

                if (isdefaultRow == null)
                {
                    
                    await _repositoryStudentStatus.InsertAsync(entity);

                    var dupObj = new
                    {
                        IsDuplicate = false,
                        IsDefaultDuplicate = false
                    };

                    return dupObj;

                }
                else if (isdefaultRow != null && entity.IsDefault)
                {
                    var dupObj = new
                    {
                        IsDuplicate = false,
                        IsDefaultDuplicate = true
                    };
                    return dupObj;
                }
                else
                {

                    await _repositoryStudentStatus.InsertAsync(entity);

                    var dupObj = new
                    {
                        IsDuplicate = false,
                        IsDefaultDuplicate = false
                    };

                    return dupObj;
                }

            }
            else
            {
                var dupObj = new
                {
                    IsDuplicate = true,
                    IsDefaultDuplicate = false
                };

                return dupObj;
            }
        }

        public async Task CreateBulk(List<StudentStatus> inputs, int tenantId)
        {

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var entity in inputs)
                {
                    entity.TenantId = tenantId;

                    await _repositoryStudentStatus.InsertAsync(entity);
                }

            }

        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryStudentStatus.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositoryStudentStatus.DeleteAsync(id);
        }

        public async Task<object> Update(StudentStatus entity)
        {

            var isdefaultRow = await _repositoryStudentStatus.FirstOrDefaultAsync(x => x.IsDefault);
            if (isdefaultRow == null)
            {
                
                await _repositoryStudentStatus.UpdateAsync(entity);

                var dupObj = new
                {
                    IsDuplicate = false,
                    IsDefaultDuplicate = false
                };

                return dupObj;
            }
            else if (isdefaultRow != null && entity.IsDefault)
            {
                var dupObj = new
                {
                    IsDuplicate = false,
                    IsDefaultDuplicate = true
                };
                return dupObj;
            }
            else
            {
                 
                await _repositoryStudentStatus.UpdateAsync(entity);

                var dupObj = new
                {
                    IsDuplicate = false,
                    IsDefaultDuplicate = false
                };

                return dupObj;
            }



        }

    }
}
