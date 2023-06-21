using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Currencies
{
   public class CurrencyManager: Abp.Domain.Services.DomainService, ICurrencyManager
    {
        private readonly IRepository<Currency, Guid> _repositoryCurrency;

        public CurrencyManager(IRepository<Currency, Guid> repositoryCurrency)
        {
            _repositoryCurrency = repositoryCurrency;
        }

        public async Task<object> Create(Currency entity)
        {
            var datta = await _repositoryCurrency.FirstOrDefaultAsync(x => x.CurrencyName == entity.CurrencyName);

            if (datta == null)
            {
                //entity.Rates.Add(new CurrencyRate
                //{

                //    Id = Guid.NewGuid(),
                //    BuyingRate = entity.BuyRate,
                //    CreatedAt = DateTime.UtcNow,
                //    SellingRate = entity.SellRate
                //});
                await _repositoryCurrency.InsertAsync(entity);

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

        public async Task Update(Currency entity)
        {

            var currencyInfo = await _repositoryCurrency.FirstOrDefaultAsync(entity.Id);

            if (currencyInfo != null)
            {
                currencyInfo.Rates.Add(new CurrencyRate { 
                
                     Id = Guid.NewGuid(), BuyingRate = entity.BuyRate, CreatedAt = DateTime.UtcNow, SellingRate = entity.SellRate
                });

                currencyInfo.CurrencyName = entity.CurrencyName;
                currencyInfo.CurrencyCode = entity.CurrencyCode;
                currencyInfo.MinorName = entity.MinorName;
                currencyInfo.BuyRate = entity.BuyRate;
                currencyInfo.SellRate = entity.SellRate;
                await _repositoryCurrency.UpdateAsync(currencyInfo);


            }

        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryCurrency.GetAsync(id);
        }

        public async Task<Currency> GetAsync(string currencyName)
        {
            return await _repositoryCurrency.FirstOrDefaultAsync(x=> !string.IsNullOrEmpty(currencyName) && x.CurrencyName.ToLower() == currencyName.Trim().ToLower() );
        }

        public async Task<object> ListAll()
        {
            return await _repositoryCurrency.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryCurrency.DeleteAsync(id);

        }
    }
}
