using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.ScheduledTours
{
    public class ScheduledTourManager: Abp.Domain.Services.DomainService, IScheduledTourManager
    {
        private readonly IRepository<ScheduledTour, Guid> _repositoryScheduledTour;

        public ScheduledTourManager(IRepository<ScheduledTour, Guid> repositorScheduledTour)
        {
            _repositoryScheduledTour = repositorScheduledTour;
        }


        public async Task<object> ListAll()
        {
            return await _repositoryScheduledTour.GetAllListAsync();
        }


        public async Task Create(ScheduledTour entity)
        {
            await _repositoryScheduledTour.InsertAsync(entity);
        }


        public async Task Delete(Guid Id)
        {
            await _repositoryScheduledTour.DeleteAsync(Id);
        }
    }
}
