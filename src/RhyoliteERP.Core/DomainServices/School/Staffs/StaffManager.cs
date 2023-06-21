using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Staffs
{
   public class StaffManager: Abp.Domain.Services.DomainService, IStaffManager
    {

        private readonly IRepository<Staff, Guid> _repositoryStaff;
        private readonly IRepository<EmployeeBioData, Guid> _repositoryEmployeeBioData;
        private readonly IRepository<Country, Guid> _repositoryCountry;
        private readonly IRepository<StudentStatus, Guid> _repositoryStudentStatus;
        public StaffManager(IRepository<Staff, Guid> repositoryStaff, IRepository<EmployeeBioData, Guid> repositoryEmployeeBioData, IRepository<Country, Guid> repositoryCountry, IRepository<StudentStatus, Guid> repositoryStudentStatus)
        {
            _repositoryStaff = repositoryStaff;
            _repositoryEmployeeBioData = repositoryEmployeeBioData;
            _repositoryCountry = repositoryCountry;
            _repositoryStudentStatus = repositoryStudentStatus;
        }


        public async Task<object> Create(Staff entity)
        {
            var datta = await _repositoryStaff.FirstOrDefaultAsync(x => x.StaffIdentifier == entity.StaffIdentifier);
            if (datta == null)
            {
                var countryInfo = await _repositoryCountry.FirstOrDefaultAsync(x=>x.Id == entity.NationalityId);
                var statusInfo = await _repositoryStudentStatus.FirstOrDefaultAsync(entity.StaffStatusId);

                if (countryInfo != null)
                {
                    entity.NationalityName = countryInfo.Nationality;
                }

                if (statusInfo != null)
                {
                    entity.StaffStatusName = statusInfo.Name;
                }

                var staffEntryResult = await _repositoryStaff.InsertAsync(entity);

                // create as employee in Payroll.
                var employee = new EmployeeBioData
                {
                    EmployeeIdentifier = entity.StaffIdentifier,
                    FirstName = entity.FirstName,
                    OtherName = entity.OtherName,
                    LastName = entity.LastName,
                    EmployeePhoto = entity.StaffImage,
                    Gender = entity.Gender,
                    StatusId = statusInfo == null ? Guid.Empty : entity.StaffStatusId,
                    StatusName = statusInfo == null ? "Active" : entity.StaffStatusName,
                    CityOrLocation = entity.CityOrLocation,
                    DepartmentId = Guid.Empty,
                    CategoryId = Guid.Empty,
                    DateAppointed = entity.DateEmployed,
                    SalaryGradeId = Guid.Empty,
                    SalaryNotchId = Guid.Empty,
                    TaxIdentificationNo = "",
                    DateOfBirth = entity.DateOfBirth,
                    PersonalEmail = entity.EmailAddress,
                    MaritalStatus = entity.MaritalStatus,
                    ReligionId = Guid.Empty,
                    NationalityId = entity.NationalityId,
                    Hometown = "",
                    PrimaryPhoneNumber = entity.PrimaryPhone,
                    SecondaryPhoneNumber = entity.SecondaryPhone,
                    CompanyEmail = entity.EmailAddress,
                    ResidenceAddress = entity.HomeAddress,
                    TenantId = entity.TenantId,
                    NationalIdentificationNo = "",
                    NationalHealthInsuranceNo = "",
                    PassportNo = null,
                    PassportIssueDate = null,
                    LicenseIssueDate = null,
                    PassportExpiryDate = null,
                    DriverLicenseNo = null,
                    LicenseExpiryDate = null,
                    VotersIDNo = null,
                    Height = 1,
                    Weight = 50,
                    Languages = null,
                    Interests = null,
                    HealthIssues = null,
                    LeaveDaysEntitled = 15,
                    MedicalExpensesLimit = 0,
                    UserId = 0,
                    Id = staffEntryResult.Id
                };
                await _repositoryEmployeeBioData.InsertAsync(employee);

                return new
                {
                    code = 200,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate Staff ID's are not allowed."
                };
            }
        }


        public async Task<IEnumerable<object>> ListAll(bool isTeachingStaff)
        {
            return await _repositoryStaff.GetAllListAsync(x=>x.IsTeachingStaff == isTeachingStaff);

        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryStaff.GetAsync(id);
        }
        public async Task Delete(Guid id)
        {
            await _repositoryStaff.DeleteAsync(id);

            //delete from employees
            await _repositoryEmployeeBioData.DeleteAsync(id);
        }

        public async Task Update(Staff entity)
        {
            var statusInfo = await _repositoryStudentStatus.FirstOrDefaultAsync(entity.StaffStatusId);

            var countryInfo = await _repositoryCountry.FirstOrDefaultAsync(x => x.Id == entity.NationalityId);
            
            if (countryInfo != null)
            {
                entity.NationalityName = countryInfo.Nationality;
            }

            if (statusInfo != null)
            {
                entity.StaffStatusName = statusInfo.Name;

            }
            await _repositoryStaff.UpdateAsync(entity);

            // update as employee in Payroll

            var employee = new EmployeeBioData
            {
                EmployeeIdentifier = entity.StaffIdentifier,
                FirstName = entity.FirstName,
                OtherName = entity.OtherName,
                LastName = entity.LastName,
                EmployeePhoto = entity.StaffImage,
                Gender = entity.Gender,
                CityOrLocation = entity.CityOrLocation,
                DepartmentId = Guid.Empty,
                CategoryId = Guid.Empty,
                DateAppointed = entity.DateEmployed,
                SalaryGradeId = Guid.Empty,
                SalaryNotchId = Guid.Empty,
                TaxIdentificationNo = "",
                DateOfBirth = entity.DateOfBirth,
                StatusId = statusInfo == null ? Guid.Empty : entity.StaffStatusId,
                StatusName = statusInfo == null ? "Active" : entity.StaffStatusName,
                PersonalEmail = entity.EmailAddress,
                MaritalStatus = entity.MaritalStatus,
                ReligionId = Guid.Empty,
                NationalityId = entity.NationalityId,
                Hometown = "",
                PrimaryPhoneNumber = entity.PrimaryPhone,
                SecondaryPhoneNumber = entity.SecondaryPhone,
                CompanyEmail = entity.EmailAddress,
                ResidenceAddress = entity.HomeAddress,
                TenantId = entity.TenantId,
                NationalIdentificationNo = "",
                NationalHealthInsuranceNo = "",
                PassportNo = null,
                PassportIssueDate = null,
                LicenseIssueDate = null,
                PassportExpiryDate = null,
                DriverLicenseNo = null,
                LicenseExpiryDate = null,
                VotersIDNo = null,
                Height = 1,
                Weight = 50,
                Languages = null,
                Interests = null,
                HealthIssues = null,
                LeaveDaysEntitled = 15,
                MedicalExpensesLimit = 0,
                UserId = 0,
                Id = entity.Id
            };
            await _repositoryEmployeeBioData.UpdateAsync(employee);


        }

    }
}
