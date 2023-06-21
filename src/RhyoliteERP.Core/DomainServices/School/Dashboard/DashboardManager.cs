using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Dashboard
{
   public class DashboardManager: Abp.Domain.Services.DomainService, IDashboardManager
    {

        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IRepository<AcademicYear, Guid> _repositoryAcademicYear;
        private readonly IRepository<Staff, Guid> _repositoryStaff;
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;
        private readonly IRepository<StudentBill, Guid> _repositoryStudentBill;
        public DashboardManager(IRepository<Student, Guid> repositoryStudent, IRepository<Staff, Guid> repositoryStaff, IRepository<SchoolProfile, Guid> repositorySchoolProfile, IRepository<StudentBill, Guid> repositoryStudentBill, IRepository<AcademicYear, Guid> repositoryAcademicYear)
        {
            _repositoryStudent = repositoryStudent;
            _repositoryStaff = repositoryStaff;
            _repositorySchoolProfile = repositorySchoolProfile;
            _repositoryStudentBill = repositoryStudentBill;
            _repositoryAcademicYear = repositoryAcademicYear;
        }


        public async Task<Dictionary<string, object>> GetStudentGenderDistribution()
        {
            var students = await _repositoryStudent.GetAllListAsync();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            int males = students.Count(a => a.Gender == "Male");
            int females = students.Count(a => a.Gender == "Female");
            int totalPopulation = students.Count;

            string maleLabel = $"{Math.Round(totalPopulation != 0 ? ((decimal)males / totalPopulation) * 100 : 0, 2)}% Males";
            string femaleLabel = $"{Math.Round(totalPopulation != 0 ? ((decimal)females / totalPopulation) * 100 : 0, 2)}% Females";

            keyValuePairs.Add("maleInfo", new { label = maleLabel, nos = males });
            keyValuePairs.Add("femaleInfo", new { label = femaleLabel, nos = females });
            return keyValuePairs;
        }

        public async Task<Dictionary<string, object>> GetStudentGenderDistribution(Guid classId)
        {
            var students = await _repositoryStudent.GetAllListAsync(x => x.ClassId == classId);
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            int males = students.Count(a => a.Gender == "Male");
            int females = students.Count(a => a.Gender == "Female");
            int totalPopulation = students.Count;

            string maleLabel = $"{Math.Round(totalPopulation != 0 ? ((decimal)males / totalPopulation) * 100 : 0, 2)}% Males";
            string femaleLabel = $"{Math.Round(totalPopulation != 0 ? ((decimal)females / totalPopulation) * 100 : 0, 2)}% Females";

            keyValuePairs.Add("maleInfo", new { label = maleLabel, nos = males });
            keyValuePairs.Add("femaleInfo", new { label = femaleLabel, nos = females });
            return keyValuePairs;
        }

        public async Task<Dictionary<string, object>> GetStaffGenderDistribution()
        {
            var staffs = await _repositoryStaff.GetAllListAsync();
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            int males = staffs.Count(a => a.Gender == "Male");
            int females = staffs.Count(a => a.Gender == "Female");
            int totalPopulation = staffs.Count;

            string maleLabel = totalPopulation > 0 ? $"{Math.Round(((decimal)males / totalPopulation) * 100, 2)}% Males" : $"{0}% Males";
            string femaleLabel = totalPopulation > 0 ? $"{Math.Round(((decimal)females / totalPopulation) * 100, 2)}% Females" : $"{0}% Females";

            keyValuePairs.Add("maleInfo", new { label = maleLabel, nos = males });
            keyValuePairs.Add("femaleInfo", new { label = femaleLabel, nos = females });
            return keyValuePairs;

        }
    
        public async Task<IEnumerable<object>> GetPayments()
        {
            var schProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (schProfile != null)
            {
                var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(schProfile.CurrentAcademicYearId);
                var terms = academicYearInfo.Terms;
                var bills = await _repositoryStudentBill.GetAllListAsync(a => a.AcademicYearId == schProfile.CurrentAcademicYearId);

                return (from u1 in bills
                        join u2 in terms on u1.TermId equals u2.Id
                        group u1 by new { u2.Name } into p
                        select new
                        {
                            TermName = p.Key.Name,
                            TotalBills = (int)p.Sum(x => x.BillAmount),
                            TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                            TotalArreas = (int)p.Sum(x => x.BillBalance)
                        }).ToList();

            }
            else
            {
                var academicYearInfo = await _repositoryAcademicYear.GetAll().FirstOrDefaultAsync();
                if (academicYearInfo != null)
                {
                    var terms = academicYearInfo.Terms;
                    var bills = await _repositoryStudentBill.GetAllListAsync(a => a.CreationTime.Year == DateTime.Now.Year);

                    return (from u1 in bills
                            join u2 in terms on u1.TermId equals u2.Id
                            group u1 by new { u2.Name } into p
                            select new
                            {
                                TermName = p.Key.Name,
                                TotalBills = (int)p.Sum(x => x.BillAmount),
                                TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                                TotalArreas = (int)p.Sum(x => x.BillBalance)
                            }).ToList();

                }

                return null;
            }


        }

        public async Task<IEnumerable<object>> GetPaymentsByClass(Guid classId)
        {
            var schProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (schProfile != null)
            {
                var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(schProfile.CurrentAcademicYearId);
                var terms = academicYearInfo.Terms;
                var bills = await _repositoryStudentBill.GetAllListAsync(a => a.AcademicYearId == schProfile.CurrentAcademicYearId && a.ClassId == classId);

                return (from u1 in bills
                        join u2 in terms on u1.TermId equals u2.Id
                        group u1 by new { u2.Name } into p
                        select new
                        {
                            TermName = p.Key.Name,
                            TotalBills = (int)p.Sum(x => x.BillAmount),
                            TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                            TotalArreas = (int)p.Sum(x => x.BillBalance)
                        }).ToList();

            }
            else
            {
                var academicYearInfo = await _repositoryAcademicYear.GetAll().FirstOrDefaultAsync();
                var terms = academicYearInfo.Terms;
                var bills = await _repositoryStudentBill.GetAllListAsync(a => a.CreationTime.Year == DateTime.Now.Year);

                return (from u1 in bills
                        join u2 in terms on u1.TermId equals u2.Id
                        group u1 by new { u2.Name } into p
                        select new
                        {
                            TermName = p.Key.Name,
                            TotalBills = (int)p.Sum(x => x.BillAmount),
                            TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                            TotalArreas = (int)p.Sum(x => x.BillBalance)

                        }).ToList();

            }

        }

        public async Task<IEnumerable<object>> GetMonthlyPayments()
        {
            var schProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (schProfile != null)
            {
                var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(schProfile.CurrentAcademicYearId);
                var terms = academicYearInfo.Terms;
                var bills = await _repositoryStudentBill.GetAllListAsync(a => a.CreationTime.Year == DateTime.Now.Year);

                return (from u1 in bills
                        let Month = u1.CreationTime.ToString("MMM")
                        group u1 by new { Month } into p
                        select new
                        {
                            p.Key.Month,
                            TotalBills = (int)p.Sum(x => x.BillAmount),
                            TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                            TotalArreas = (int)p.Sum(x => x.BillBalance)
                        }).ToList();

            }

            return new List<object>() ;
            
        }


        public async Task<IEnumerable<object>> GetMonthlyPayments(Guid classId)
        {
            var schProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (schProfile != null)
            {
                var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(schProfile.CurrentAcademicYearId);
                var terms = academicYearInfo.Terms;
                var bills = await _repositoryStudentBill.GetAllListAsync(a => a.CreationTime.Year == DateTime.Now.Year && a.ClassId == classId);

                return (from u1 in bills
                        let Month = u1.CreationTime.ToString("MMM")
                        group u1 by new { Month } into p
                        select new
                        {
                            p.Key.Month,
                            TotalBills = (int)p.Sum(x => x.BillAmount),
                            TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                            TotalArreas = (int)p.Sum(x => x.BillBalance)
                        }).ToList();

            }

            return new List<object>();

        }

        public async Task<IEnumerable<object>> GetPaymentsTillDate()
        {
            var schProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (schProfile != null)
            {
                var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(schProfile.CurrentAcademicYearId);
                var terms = academicYearInfo.Terms;
                var bills = await _repositoryStudentBill.GetAllListAsync();

                return (from u1 in bills
                        let Year = u1.CreationTime.ToString("yyyy")
                        group u1 by new { Year } into p
                        select new
                        {
                            p.Key.Year,
                            TotalBills = (int)p.Sum(x => x.BillAmount),
                            TotalPayments = (int)p.Sum(x => x.BillAmount - x.BillBalance),
                            TotalArreas = (int)p.Sum(x => x.BillBalance)
                        }).ToList();

            }

            return new List<object>();
        }

    }
}
