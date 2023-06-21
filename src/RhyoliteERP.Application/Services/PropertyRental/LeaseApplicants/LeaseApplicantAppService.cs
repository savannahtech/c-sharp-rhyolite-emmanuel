using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.LeaseApplicants;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.LeaseApplicants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeaseApplicants
{
    public class LeaseApplicantAppService: RhyoliteERPAppServiceBase, ILeaseApplicantAppService
    {

        private readonly ILeaseApplicantManager _leaseApplicantManager;
        private readonly IMapper _mapper;

        public LeaseApplicantAppService(ILeaseApplicantManager leaseApplicantManager, IMapper mapper)
        {
            _leaseApplicantManager = leaseApplicantManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _leaseApplicantManager.ListAll();
        }


        public async Task Create(CreateLeaseApplicantInput input)
        {
            var obj = _mapper.Map<LeaseApplicant>(input);
            await _leaseApplicantManager.Create(obj);
        }


        public async Task Delete(Guid Id)
        {
            await _leaseApplicantManager.Delete(Id);
        }


    }
}
