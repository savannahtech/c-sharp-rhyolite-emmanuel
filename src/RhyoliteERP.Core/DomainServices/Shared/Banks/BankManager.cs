using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.School.AcademicYears.Dto;
using RhyoliteERP.DomainServices.Shared.Banks.Dto;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Banks
{
    public class BankManager: Abp.Domain.Services.DomainService, IBankManager
    {
        private readonly IRepository<Bank, Guid> _repositoryBank;

        public BankManager(IRepository<Bank, Guid> repositoryBank)
        {
            _repositoryBank = repositoryBank;
        }

        public async Task<object> Create(Bank entity)
        {
            var datta = await _repositoryBank.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                entity.Name = entity.Name.Trim();

                await _repositoryBank.InsertAsync(entity);

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
                    message = "Duplicate records are not allowed."
                };
            }
        }

        public async Task Update(Bank entity)
        {
            entity.Name = entity.Name.Trim();

            await _repositoryBank.UpdateAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryBank.GetAllListAsync();
        }

        public async Task<Bank> GetAsync(string bankName)
        {
            return await _repositoryBank.FirstOrDefaultAsync(x=> !string.IsNullOrEmpty(bankName) && x.Name.ToLower() == bankName.Trim().ToLower() );

        }

        public async Task Delete(Guid id)
        {
            await _repositoryBank.DeleteAsync(id);

        }

        // branches
        public async Task<object> CreateBranch(BranchInput input)
        {
            var bankData = await _repositoryBank.FirstOrDefaultAsync(input.BankId);
            var branchList = bankData.BankBranches;
            var branchInfo = branchList.FirstOrDefault(x => x.Id == input.Id);

            if (branchInfo == null)
            {
                bankData.BankBranches.Add(new BankBranch
                {
                    Id = input.Id,
                    ContactNo = input.ContactNo,
                    ContactPerson = input.ContactPerson,
                    Email = input.Email,
                    Name = input.Name,

                });

                await _repositoryBank.UpdateAsync(bankData);

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
                    message = "Duplicate records are not allowed."
                };

            }

        }


        public async Task UpdateBranch(BranchInput input)
        {
            var bankData = await _repositoryBank.FirstOrDefaultAsync(input.BankId);
            var branchList = bankData.BankBranches;
            var branchInfo = branchList.FirstOrDefault(x => x.Id == input.Id);
            branchList.Remove(branchInfo);

            branchInfo.Name = input.Name;
            branchInfo.ContactPerson = input.ContactPerson;
            branchInfo.Email = input.Email;
            branchInfo.ContactNo = input.ContactNo;

            branchList.Add(branchInfo);

            bankData.BankBranches = branchList;

            await _repositoryBank.UpdateAsync(bankData);
        }


        public async Task DeleteBranch(BranchInput input)
        {
            var bankData = await _repositoryBank.FirstOrDefaultAsync(input.BankId);
            var branchList = bankData.BankBranches;
            var branchInfo = branchList.FirstOrDefault(x => x.Id == input.Id);
            branchList.Remove(branchInfo);

            bankData.BankBranches = branchList;

            await _repositoryBank.UpdateAsync(bankData);

        }

       
    }
}
