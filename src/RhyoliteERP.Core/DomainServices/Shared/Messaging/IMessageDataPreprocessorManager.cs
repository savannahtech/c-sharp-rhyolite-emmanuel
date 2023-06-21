using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Messaging
{
   public  interface IMessageDataPreprocessorManager: Abp.Domain.Services.IDomainService
    {
        Task<List<object>> DoPreprocess(string message, Guid recipientId, string recipientType);
        Task<List<object>> DoProcessStudentReceipt(Guid id, Guid statementId);
        Task<List<object>> PersonalizedMessagePreprocessor(string message, List<string> recipientList);
    }
}
