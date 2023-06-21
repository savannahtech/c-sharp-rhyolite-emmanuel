using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.PastQuestions
{
   public interface IPastQuestionManager : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(PastQuestion input);
        Task Update(PastQuestion input);
        Task Delete(Guid Id);
    }
}
