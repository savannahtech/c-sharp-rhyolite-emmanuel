using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.CancelledBills
{
   public class CancelledBillManager: Abp.Domain.Services.DomainService, ICancelledBillManager
    {

        private readonly IRepository<CancelledBill, Guid> _repositoryCancelledBill;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;

        public CancelledBillManager(IRepository<CancelledBill, Guid> repositoryCancelledBill, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositoryCancelledBill = repositoryCancelledBill;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }

        public async Task CreateBatch(List<CancelledBill> bills)
        {

            foreach (CancelledBill entity in bills)
            {

                var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId);
                entity.AcademicYearName = basicInfo.AcademicYearName;
                entity.TermName = basicInfo.TermName;
                entity.ClassName = basicInfo.ClassName;

                await _repositoryCancelledBill.InsertAsync(entity);

            }

        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryCancelledBill.GetAllListAsync(x=> x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillTypeId == billTypeId);

        }

    }


}
