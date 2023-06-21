using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.UnitReservations
{
    public class UnitReservationManager : Abp.Domain.Services.DomainService, IUnitReservationManager
    {

        private readonly IRepository<UnitReservation, Guid> _repositoryUnitReservation;

        public UnitReservationManager(IRepository<UnitReservation, Guid> repositoryUnitReservation)
        {
            _repositoryUnitReservation = repositoryUnitReservation;
        }

        public async Task Create(UnitReservation entity)
        {
            await _repositoryUnitReservation.InsertAsync(entity);

        }


        public async Task<object> ListAll()
        {
            return await _repositoryUnitReservation.GetAllListAsync();
        }


        public async Task Delete(Guid id)
        {
            await _repositoryUnitReservation.DeleteAsync(id);
        }


    }
}
