using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.ResidentAccounts
{
    public interface IResidentAccountManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(ResidentAccount entity);

    }
}
