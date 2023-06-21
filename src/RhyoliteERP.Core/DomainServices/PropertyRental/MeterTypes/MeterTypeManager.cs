using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.MeterTypes
{
    public class MeterTypeManager: Abp.Domain.Services.DomainService, IMeterTypeManager
    {
        private readonly IRepository<MeterType, Guid> _repositoryMeterType;

        public MeterTypeManager(IRepository<MeterType, Guid> repositoryMeterType)
        {
            _repositoryMeterType = repositoryMeterType;
        }

        public async Task<object> Create(MeterType entity)
        {
            var datta = await _repositoryMeterType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryMeterType.InsertAsync(entity);

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

        public async Task Update(MeterType entity)
        {
            await _repositoryMeterType.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryMeterType.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryMeterType.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryMeterType.DeleteAsync(id);
        }
    }
}
