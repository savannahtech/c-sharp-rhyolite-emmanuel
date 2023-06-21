using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.CostCenters
{
    public interface ICostCenterManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(CostCenter input);
        Task Update(CostCenter input);
        Task Delete(Guid Id);
    }
}
