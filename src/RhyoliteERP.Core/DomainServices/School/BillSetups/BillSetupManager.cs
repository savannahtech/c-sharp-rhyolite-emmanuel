using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillSetups
{
   public class BillSetupManager : Abp.Domain.Services.DomainService, IBillSetupManager
    {
        private readonly IRepository<BillSetup, Guid> _repositoryBillSetup;
        private readonly IRepository<BillType, Guid> _repositoryBillType;
        private readonly IRepository<FeesDescription, Guid> _repositoryFeesDescription;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;

        public BillSetupManager(IRepository<BillSetup, Guid> repositoryBillSetup, IBasicAcademicInfoManager basicAcademicInfoManager, IRepository<BillType, Guid> repositoryBillType, IRepository<FeesDescription, Guid> repositoryFeesDescription)
        {
            _repositoryBillSetup = repositoryBillSetup;
            _basicAcademicInfoManager = basicAcademicInfoManager;
            _repositoryBillType = repositoryBillType;
            _repositoryFeesDescription = repositoryFeesDescription;
        }


        public async Task<object> Create(BillSetup entity)
        {
            var billTypeInfo = await _repositoryBillType.FirstOrDefaultAsync(x=>x.Id == entity.BillTypeId);

            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo( entity.AcademicYearId ,entity.TermId , entity.ClassId);

            var datta = await _repositoryBillSetup.FirstOrDefaultAsync(x => x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId && x.ClassId == entity.ClassId && x.BillTypeId == entity.BillTypeId);

            var billDetail = entity.Details.FirstOrDefault();

            var feeDescriptionInfo = await _repositoryFeesDescription.FirstOrDefaultAsync(x => x.Id == billDetail.FeeId);

            if (datta != null)
            {
                // already exists
                datta.AcademicYearName = basicInfo.AcademicYearName;
                datta.TermName = basicInfo.TermName;
                datta.ClassName = basicInfo.ClassName;
                datta.BillTypeId = entity.BillTypeId;
                if (billTypeInfo != null)
                {
                    datta.BillTypeName = billTypeInfo.Name;
                }
                datta.TotalBillAmount = entity.TotalBillAmount;

                var billDetails = datta.Details;

                var billDetailInfo = billDetails.FirstOrDefault(x=>x.FeeId == entity.Details[0].FeeId);
                if (billDetailInfo != null)
                {
                    billDetails.Remove(billDetailInfo);

                }


                billDetails.Add(new BillSetupDetail
                {
                    Id = billDetail.Id,
                    FeeAmount = billDetail.FeeAmount,
                    FeeId = billDetail.FeeId,
                    FeeName = feeDescriptionInfo.Description,
                    BillTypeId = entity.BillTypeId,
                    BillTypeName = billTypeInfo == null ? "" : billTypeInfo.Name,

                });


                datta.Details = billDetails;

                return await _repositoryBillSetup.UpdateAsync(datta);

            }
            else
            {
                var obj = new BillSetup
                {
                    AcademicYearId = entity.AcademicYearId,
                    TermId = entity.TermId,
                    ClassId = entity.ClassId,
                    ClassName = basicInfo.ClassName,
                    TermName = basicInfo.TermName,
                    AcademicYearName = basicInfo.AcademicYearName,
                    BillTypeId = entity.BillTypeId,
                    TotalBillAmount = entity.TotalBillAmount,
                    Details = new List<BillSetupDetail> { 
                    
                    new BillSetupDetail
                    {
                         Id = billDetail.Id,
                         FeeAmount = billDetail.FeeAmount,
                         FeeId = billDetail.FeeId,
                         FeeName = feeDescriptionInfo.Description,
                         BillTypeId = entity.BillTypeId,
                         BillTypeName = billTypeInfo == null ? "": billTypeInfo.Name,

                    }
                    },
                    TenantId = entity.TenantId,
                };

                return await _repositoryBillSetup.InsertAsync(obj);
            }
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryBillSetup.GetAllListAsync(x=>x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillTypeId == billTypeId);
        }

        public async Task<object> GetAsync(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryBillSetup.FirstOrDefaultAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillTypeId == billTypeId);
        }

        public async Task Delete(Guid Id)
        {
            await _repositoryBillSetup.DeleteAsync(Id);
        }

        public async Task DeleteDetail(Guid id, Guid headerId)
        {
            var billSetup = await _repositoryBillSetup.GetAsync(headerId);

            var detailInfo =  billSetup.Details.FirstOrDefault(a=>a.Id == id);

            billSetup.Details.Remove(detailInfo);

            await _repositoryBillSetup.UpdateAsync(billSetup);
        }
    }
}
