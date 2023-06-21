using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.ResidentAccounts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.ResidentAccounts
{
    public interface IResidentAccountAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateResidentAccountInput entity);
    }
}
