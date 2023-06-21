using RhyoliteERP.Services.PropertyRental.LeaseApplicants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeaseApplicants
{
    public interface ILeaseApplicantAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateLeaseApplicantInput input);
        Task Delete(Guid Id);
    }
}
