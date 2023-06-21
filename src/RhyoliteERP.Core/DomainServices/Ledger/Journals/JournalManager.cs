using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.Journals
{
   public class JournalManager : Abp.Domain.Services.DomainService, IJournalManager
    {
        private readonly IRepository<Journal, Guid> _repositoryJournal;

        public JournalManager(IRepository<Journal, Guid> repositoryJournal)
        {
            _repositoryJournal = repositoryJournal;
        }

        public async Task Create(Journal entity)
        {
            await _repositoryJournal.InsertAsync(entity);
        }


        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryJournal.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryJournal.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryJournal.DeleteAsync(id);
        }

        public async Task BatchDelete(List<Guid> Ids)
        {
            for (int i = 0; i < Ids.Count; i++)
            {
                await _repositoryJournal.DeleteAsync(Ids[0]);
            }
        }
    }
}
