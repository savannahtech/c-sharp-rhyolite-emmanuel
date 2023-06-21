using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Messaging
{
   public class MessageDataPreprocessorManager: IMessageDataPreprocessorManager
    {
        private readonly IRepository<StudentBill, Guid> _repositoryStudentBill;
        private readonly IRepository<Parent, Guid> _repositoryParent;
        private readonly IRepository<StudentParent, Guid> _repositoryStudentParent;
        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IRepository<Staff, Guid> _repositoryStaff;
        private readonly IRepository<EmployeeBioData, Guid> _repositoryEmployeeBioData;
        private readonly IRepository<StudentStatement, Guid> _repositoryStudentStatement;
        private readonly IRepository<LeaseTenant, Guid> _repositoryLeaseTenant;

        public MessageDataPreprocessorManager(IRepository<StudentBill, Guid> repositoryStudentBill, IRepository<Parent, Guid> repositoryParent, IRepository<StudentParent, Guid> repositoryStudentParent, IRepository<Student, Guid> repositoryStudent, IRepository<Staff, Guid> repositoryStaff, IRepository<StudentStatement, Guid> repositoryStudentStatement, IRepository<EmployeeBioData, Guid> repositoryEmployeeBioData, IRepository<LeaseTenant, Guid> repositoryLeaseTenant)
        {
            _repositoryStudentBill = repositoryStudentBill;
            _repositoryParent = repositoryParent;
            _repositoryStudentParent = repositoryStudentParent;
            _repositoryStudent = repositoryStudent;
            _repositoryStaff = repositoryStaff;
            _repositoryStudentStatement = repositoryStudentStatement;
            _repositoryEmployeeBioData = repositoryEmployeeBioData;
            _repositoryLeaseTenant = repositoryLeaseTenant;
        }

        //for parent,employee, school staff, tenant... 
        public async Task<List<object>> DoPreprocess(string message, Guid recipientId, string recipientType)
        {
            List<object> messagesList = new List<object>();

            switch (recipientType)
            {
                case "parent":

                    if (recipientId != Guid.Empty)
                    {
                        var parentInfo = await _repositoryParent.FirstOrDefaultAsync(recipientId);

                        decimal billAmount = 0;
                        decimal billBalance = 0;
                        string billNo = string.Empty;
                        string processedMessage = string.Empty;

                        var studentName = string.Empty;
                        var studentID = string.Empty;
                        var firstGuardianName = string.Empty;
                        var secondGuardianName = string.Empty;
                        var className = string.Empty;

                        var studentParentInfo = await _repositoryStudentParent.GetAllListAsync(x => x.ParentId == parentInfo.Id);

                        var studentIdList = studentParentInfo.Select(x => x.StudentId).ToList();

                        if (message.Contains("["))
                        {
                            foreach (var studentId in studentIdList)
                            {

                                if (message.Contains("[BillAmount]") || message.Contains("[BillBalance]") || message.Contains("[BillNo]"))
                                {
                                    var studentBillInfo = await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentId);

                                    if (studentBillInfo != null && studentBillInfo.Any())
                                    {
                                        billAmount = studentBillInfo.LastOrDefault().BillAmount;
                                        billBalance = studentBillInfo.Sum(a => a.BillBalance);
                                        billNo = studentBillInfo.LastOrDefault().BillNo;
                                    }
                                }


                                if (message.Contains("[StudentName]") || message.Contains("[StudentID]") || message.Contains("[ClassName]"))
                                {
                                    var studentInfo = await _repositoryStudent.FirstOrDefaultAsync(studentId);

                                    if (studentInfo != null)
                                    {
                                        studentName = studentInfo.MiddleName == null ? $"{studentInfo.LastName} {studentInfo.FirstName}" : $"{studentInfo.LastName} {studentInfo.FirstName} {studentInfo.MiddleName}";
                                        studentID = studentInfo.StudentIdentifier;
                                        className = studentInfo.ClassName;

                                    }

                                }

                            }

                            if (message.Contains("[1stGuardianName]") || message.Contains("[2ndGuardianName]"))
                            {
                                if (parentInfo != null)
                                {
                                    firstGuardianName = parentInfo.FirstGuardianName;
                                    secondGuardianName = parentInfo.SecondGuardianName;
                                }

                            }

                            if (billAmount > 0 && billBalance > 0)
                            {
                                processedMessage = new StringBuilder(message)
                                 .Replace("[1stGuardianName]", firstGuardianName)
                                 .Replace("[2ndGuardianName]", secondGuardianName)
                                 .Replace("[StudentName]", studentName)
                                 .Replace("[StudentID]", studentID)
                                 .Replace("[BillAmount]", $"GHS {billAmount:N2}")
                                 .Replace("[BillBalance]", $"GHS {billBalance:N2}")
                                 .Replace("[BillNo]", billNo)
                                 .Replace("[ClassName]", className)
                                 .ToString();

                            }
                            else if (billAmount <= 0 && billBalance > 0)
                            {
                                processedMessage = new StringBuilder(message)
                                .Replace("[1stGuardianName]", firstGuardianName)
                                .Replace("[2ndGuardianName]", secondGuardianName)
                                .Replace("[StudentName]", studentName)
                                .Replace("[StudentID]", studentID)
                                .Replace("[BillAmount]", string.Empty)
                                .Replace("[BillBalance]", $"GHS {billBalance:N2}")
                                .Replace("[BillNo]", billNo)
                                .Replace("[ClassName]", className)
                                .ToString();

                            }
                            else if (billBalance <= 0 && billAmount > 0)
                            {
                                processedMessage = new StringBuilder(message)
                                 .Replace("[1stGuardianName]", firstGuardianName)
                                 .Replace("[2ndGuardianName]", secondGuardianName)
                                 .Replace("[StudentName]", studentName)
                                 .Replace("[StudentID]", studentID)
                                 .Replace("[BillAmount]", $"GHS {billAmount:N2}")
                                 .Replace("[BillBalance]", string.Empty)
                                 .Replace("[BillNo]", billNo)
                                 .Replace("[ClassName]", className)
                                 .ToString();

                            }
                            else if (billBalance <= 0 && billAmount <= 0)
                            {
                                processedMessage = new StringBuilder(message)
                                 .Replace("[1stGuardianName]", firstGuardianName)
                                 .Replace("[2ndGuardianName]", secondGuardianName)
                                 .Replace("[StudentName]", studentName)
                                 .Replace("[StudentID]", studentID)
                                 .Replace("[BillNo]", billNo)
                                 .Replace("[ClassName]", className)
                                 .ToString();

                            }

                            messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = parentInfo.FirstGuardianPhoneNo });

                        }
                        else
                        {
                            messagesList.Add(new { Message = message, RecipientPhoneNo = parentInfo.FirstGuardianPhoneNo });

                        }
                    }
                    else
                    {
                        var parentList = await _repositoryParent.GetAllListAsync();

                        foreach (var parentInfo in parentList)
                        {
                            decimal billAmount = 0;
                            decimal billBalance = 0;
                            string billNo = string.Empty;
                            string processedMessage = string.Empty;

                            var studentName = string.Empty;
                            var studentID = string.Empty;
                            var firstGuardianName = string.Empty;
                            var secondGuardianName = string.Empty;
                            var className = string.Empty;

                            var studentParentInfo = await _repositoryStudentParent.GetAllListAsync(x => x.ParentId == parentInfo.Id);

                            var studentIdList = studentParentInfo.Select(x => x.StudentId).ToList();

                            if (message.Contains("["))
                            {
                                foreach (var studentId in studentIdList)
                                {

                                    if (message.Contains("[BillAmount]") || message.Contains("[BillBalance]") || message.Contains("[BillNo]"))
                                    {
                                        var studentBillInfo = await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentId);

                                        if (studentBillInfo != null && studentBillInfo.Any())
                                        {
                                            billAmount = studentBillInfo.LastOrDefault().BillAmount;
                                            billBalance = studentBillInfo.Sum(a => a.BillBalance);
                                            billNo = studentBillInfo.LastOrDefault().BillNo;
                                        }
                                    }


                                    if (message.Contains("[StudentName]") || message.Contains("[StudentID]") || message.Contains("[ClassName]"))
                                    {
                                        var studentInfo = await _repositoryStudent.FirstOrDefaultAsync(studentId);

                                        if (studentInfo != null)
                                        {
                                            studentName = studentInfo.MiddleName == null ? $"{studentInfo.LastName} {studentInfo.FirstName}" : $"{studentInfo.LastName} {studentInfo.FirstName} {studentInfo.MiddleName}";
                                            studentID = studentInfo.StudentIdentifier;
                                            className = studentInfo.ClassName;

                                        }

                                    }

                                }

                                if (message.Contains("[1stGuardianName]") || message.Contains("[2ndGuardianName]"))
                                {
                                    if (parentInfo != null)
                                    {
                                        firstGuardianName = parentInfo.FirstGuardianName;
                                        secondGuardianName = parentInfo.SecondGuardianName;
                                    }

                                }

                                if (billAmount > 0 && billBalance > 0)
                                {
                                    processedMessage = new StringBuilder(message)
                                     .Replace("[1stGuardianName]", firstGuardianName)
                                     .Replace("[2ndGuardianName]", secondGuardianName)
                                     .Replace("[StudentName]", studentName)
                                     .Replace("[StudentID]", studentID)
                                     .Replace("[BillAmount]", $"GHS {billAmount:N2}")
                                     .Replace("[BillBalance]", $"GHS {billBalance:N2}")
                                     .Replace("[BillNo]", billNo)
                                     .Replace("[ClassName]", className)
                                     .ToString();

                                }
                                else if (billAmount <= 0 && billBalance > 0)
                                {
                                    processedMessage = new StringBuilder(message)
                                    .Replace("[1stGuardianName]", firstGuardianName)
                                    .Replace("[2ndGuardianName]", secondGuardianName)
                                    .Replace("[StudentName]", studentName)
                                    .Replace("[StudentID]", studentID)
                                    .Replace("[BillAmount]", string.Empty)
                                    .Replace("[BillBalance]", $"GHS {billBalance:N2}")
                                    .Replace("[BillNo]", billNo)
                                    .Replace("[ClassName]", className)
                                    .ToString();

                                }
                                else if (billBalance <= 0 && billAmount > 0)
                                {
                                    processedMessage = new StringBuilder(message)
                                     .Replace("[1stGuardianName]", firstGuardianName)
                                     .Replace("[2ndGuardianName]", secondGuardianName)
                                     .Replace("[StudentName]", studentName)
                                     .Replace("[StudentID]", studentID)
                                     .Replace("[BillAmount]", $"GHS {billAmount:N2}")
                                     .Replace("[BillBalance]", string.Empty)
                                     .Replace("[BillNo]", billNo)
                                     .Replace("[ClassName]", className)
                                     .ToString();

                                }
                                else if (billBalance <= 0 && billAmount <= 0)
                                {
                                    processedMessage = new StringBuilder(message)
                                     .Replace("[1stGuardianName]", firstGuardianName)
                                     .Replace("[2ndGuardianName]", secondGuardianName)
                                     .Replace("[StudentName]", studentName)
                                     .Replace("[StudentID]", studentID)
                                     .Replace("[BillNo]", billNo)
                                     .Replace("[ClassName]", className)
                                     .ToString();

                                }

                                messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = parentInfo.FirstGuardianPhoneNo });

                            }
                            else
                            {
                                messagesList.Add(new { Message = message, RecipientPhoneNo = parentInfo.FirstGuardianPhoneNo });

                            }


                        }

                    }

                    break;
                case "staff":

                    if (recipientId != Guid.Empty)
                    {

                        var staffInfo = await _repositoryStaff.FirstOrDefaultAsync(recipientId);

                        string staffID = string.Empty;
                        string staffName = string.Empty;
                        string processedMessage = string.Empty;

                        if (message.Contains("["))
                        {
                            if (staffInfo != null)
                            {
                                staffID = staffInfo.StaffIdentifier;
                                staffName = staffInfo.OtherName == null ? $"{staffInfo.LastName} {staffInfo.FirstName}" : $"{staffInfo.LastName} {staffInfo.FirstName} {staffInfo.OtherName}";
                            }

                            processedMessage = new StringBuilder(message)
                                       .Replace("[StaffID]", staffID)
                                       .Replace("[StaffName]", staffName)
                                       .ToString();

                            messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = staffInfo.PrimaryPhone });

                        }
                        else
                        {
                            messagesList.Add(new { Message = message, RecipientPhoneNo = staffInfo.PrimaryPhone });

                        }

                    }
                    else
                    {
                        var staffList = await _repositoryStaff.GetAllListAsync();

                        foreach (var staffInfo in staffList)
                        {
                            string staffID = string.Empty;
                            string staffName = string.Empty;
                            string processedMessage = string.Empty;


                            if (message.Contains("["))
                            {
                                if (staffInfo != null)
                                {
                                    staffID = staffInfo.StaffIdentifier;
                                    staffName = staffInfo.OtherName == null ? $"{staffInfo.LastName} {staffInfo.FirstName}" : $"{staffInfo.LastName} {staffInfo.FirstName} {staffInfo.OtherName}";
                                }

                                processedMessage = new StringBuilder(message)
                                           .Replace("[StaffID]", staffID)
                                           .Replace("[StaffName]", staffName)
                                           .ToString();

                                messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = staffInfo.PrimaryPhone });

                            }
                            else
                            {
                                messagesList.Add(new { Message = message, RecipientPhoneNo = staffInfo.PrimaryPhone });

                            }

                        }
                    }

                    break;

                case "employee":

                    if (recipientId != Guid.Empty)
                    {

                        var employeeBioData = await _repositoryEmployeeBioData.FirstOrDefaultAsync(recipientId);

                        string employeeID = string.Empty;
                        string employeeName = string.Empty;
                        string processedMessage = string.Empty;

                        if (message.Contains("["))
                        {
                            if (employeeBioData != null)
                            {
                                employeeID = employeeBioData.EmployeeIdentifier;

                                employeeName = employeeBioData.OtherName == null ? $"{employeeBioData.LastName} {employeeBioData.FirstName}" : $"{employeeBioData.LastName} {employeeBioData.FirstName} {employeeBioData.OtherName}";
                            }

                            processedMessage = new StringBuilder(message)
                                       .Replace("[EmployeeID]", employeeID)
                                       .Replace("[EmployeeName]", employeeName)
                                       .ToString();

                            messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = employeeBioData.PrimaryPhoneNumber });

                        }
                        else
                        {
                            messagesList.Add(new { Message = message, RecipientPhoneNo = employeeBioData.PrimaryPhoneNumber });

                        }

                    }
                    else
                    {
                        var employeeList = await _repositoryEmployeeBioData.GetAllListAsync();

                        foreach (var employeeInfo in employeeList)
                        {
                            string employeeID = string.Empty;
                            string employeeName = string.Empty;
                            string processedMessage = string.Empty;

                            if (message.Contains("["))
                            {
                                if (employeeInfo != null)
                                {
                                    employeeID = employeeInfo.EmployeeIdentifier;
                                    employeeName = employeeInfo.OtherName == null ? $"{employeeInfo.LastName} {employeeInfo.FirstName}" : $"{employeeInfo.LastName} {employeeInfo.FirstName} {employeeInfo.OtherName}";
                                }

                                processedMessage = new StringBuilder(message)
                                            .Replace("[EmployeeID]", employeeID)
                                            .Replace("[EmployeeName]", employeeName)
                                            .ToString();

                                messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = employeeInfo.PrimaryPhoneNumber });

                            }
                            else
                            {
                                messagesList.Add(new { Message = message, RecipientPhoneNo = employeeInfo.PrimaryPhoneNumber });
                            }

                        }
                    }

                    break;

                case "lease-tenant":

                    if (recipientId != Guid.Empty)
                    {
                        var leaseTenantInfo = await _repositoryLeaseTenant.FirstOrDefaultAsync(recipientId);

                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        string tenantName = string.Empty;

                        string processedMessage = string.Empty;

                        if (message.Contains("["))
                        {
                            if (leaseTenantInfo != null)
                            {
                                firstName = leaseTenantInfo.FirstName;
                                lastName = leaseTenantInfo.LastName;

                                tenantName = $"{leaseTenantInfo.FirstName} {leaseTenantInfo.LastName}" ;
                            }

                            processedMessage = new StringBuilder(message)
                                       .Replace("[TenantFirstName]", firstName)
                                       .Replace("[TenantLastName]", lastName)
                                       .Replace("[TenantFullName]", tenantName)
                                       .ToString();

                            messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = leaseTenantInfo.PrimaryPhoneNo });

                        }
                        else
                        {
                            messagesList.Add(new { Message = message, RecipientPhoneNo = leaseTenantInfo.PrimaryPhoneNo });

                        }
                    }
                    else
                    {
                        var leaseTenantList = await _repositoryLeaseTenant.GetAllListAsync();

                        foreach (var leaseTenant in leaseTenantList)
                        {
                            string firstName = string.Empty;
                            string lastName = string.Empty;
                            string tenantName = string.Empty;

                            string processedMessage = string.Empty;

                            if (message.Contains("["))
                            {
                                if (leaseTenant != null)
                                {
                                    firstName = leaseTenant.FirstName;
                                    lastName = leaseTenant.LastName;

                                    tenantName = $"{leaseTenant.FirstName} {leaseTenant.LastName}";
                                }


                                processedMessage = new StringBuilder(message)
                                      .Replace("[TenantFirstName]", firstName)
                                      .Replace("[TenantLastName]", lastName)
                                      .Replace("[TenantFullName]", tenantName)
                                      .ToString();

                                messagesList.Add(new { Message = processedMessage, RecipientPhoneNo = leaseTenant.PrimaryPhoneNo });

                            }
                            else
                            {
                                messagesList.Add(new { Message = message, RecipientPhoneNo = leaseTenant.PrimaryPhoneNo });
                            }

                        }
                    }

                    break;

                        default:
                    break;
            }



            return messagesList;


        }
    
        public async Task<List<object>> DoProcessStudentReceipt(Guid id, Guid statementId)
        {
            var messagesList = new List<object>();
            string messageContent = string.Empty;

            var studentStatement = await _repositoryStudentStatement.FirstOrDefaultAsync(id);

            if (studentStatement != null)
            {
                var studentParentInfo = await _repositoryStudentParent.FirstOrDefaultAsync(x=>x.StudentId == studentStatement.StudentId);

                var parentInfo = await _repositoryParent.FirstOrDefaultAsync(x => studentParentInfo != null && x.Id == studentParentInfo.ParentId);

                var statement = studentStatement.Statement.FirstOrDefault(x=> x.Id == statementId);

                if (statement != null)
                {
                    string subject = $"Payment Receipt {statement.ReferenceNo}";

                    if (statement.Balance <= 0)
                    {
                        messageContent = $"Payment of GHS {statement.Payment:N2} has been received as full payment for {studentStatement.StudentName} on {statement.ActivityDate}. You have no outstanding balance.";
                    }
                    else
                    {
                        messageContent = $"Payment of GHS {statement.Payment:N2} has been received as part payment for {studentStatement.StudentName} on {statement.ActivityDate}. Your outstanding balance is GHS {statement.Balance:N2}";
                    }

                    messagesList.Add(new { Message = messageContent, RecipientPhoneNo = parentInfo != null ? parentInfo.FirstGuardianPhoneNo: string.Empty });
                }
                

            }

            return messagesList;


        }

        public async Task<List<object>> PersonalizedMessagePreprocessor(string message, List<string> recipientList)
        {
            List<object> messagesList = new List<object>();

            foreach (var recipient in recipientList)
            {
                messagesList.Add(new { Message = message, RecipientPhoneNo = recipient });
            }

            return messagesList;
        }



        private int CalculateParts(string message)
        {
            const int creditPerPart = 1;
            const int singleMessageLength = 160;
            const int multiMessageLength = 153;
            double len = message.Length;
            if (len <= singleMessageLength)
                return creditPerPart;
            return (int)(Math.Ceiling(len / multiMessageLength) * creditPerPart);
        }
    }
}
