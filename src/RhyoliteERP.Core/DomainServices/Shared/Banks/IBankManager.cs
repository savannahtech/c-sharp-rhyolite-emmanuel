using RhyoliteERP.DomainServices.Shared.Banks.Dto;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Banks
{
   public interface IBankManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(Bank input);
        Task<Bank> GetAsync(string bankName);
        Task Update(Bank input);
        Task Delete(Guid Id);

        //branches
        Task<object> CreateBranch(BranchInput input);
        Task UpdateBranch(BranchInput input);
        Task DeleteBranch(BranchInput input);

    }
}
