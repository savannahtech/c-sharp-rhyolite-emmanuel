using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.Leases;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.LeasePayments.Dto;
using RhyoliteERP.Services.PropertyRental.Leases.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Leases
{
    public class LeaseAppService: RhyoliteERPAppServiceBase, ILeaseAppService
    {

        private readonly ILeaseManager _leaseManager;
        private readonly IMapper _mapper;

        public LeaseAppService(ILeaseManager leaseManager, IMapper mapper)
        {
            _leaseManager = leaseManager;
            _mapper = mapper;
        }


        public async Task<object> ListAll()
        {
            return await _leaseManager.ListAll();
        }

        public async Task Create(CreateLeaseInput input)
        {
            var obj = _mapper.Map<Lease>(input);
            await _leaseManager.Create(obj);
        }

        public async Task Update(UpdateLeaseInput input)
        {
            var obj = _mapper.Map<Lease>(input);
            await _leaseManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _leaseManager.Delete(Id);
        }
    }
}
