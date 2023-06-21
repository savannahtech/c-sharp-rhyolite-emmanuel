using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.IrsSignatures
{
    public interface IIrsSignatureManager : Abp.Domain.Services.IDomainService
    {
        Task Create(IrsSignature entity);
        Task<object> GetSignature();
    }
}
