using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.LeasePayments;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.LeasePayments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeasePayments
{
    public class LeasePaymentAppService: RhyoliteERPAppServiceBase, ILeasePaymentAppService
    {
        private readonly ILeasePaymentManager _leasePaymentManager;
        private readonly IMapper _mapper;

        public LeasePaymentAppService(ILeasePaymentManager leasePaymentManager, IMapper mapper)
        {
            _leasePaymentManager = leasePaymentManager;
            _mapper = mapper;
        }


        public async Task<object> ListAll()
        {
            return await _leasePaymentManager.ListAll();
        }

        public async Task Create(CreateLeasePaymentInput input)
        {
            var obj = _mapper.Map<LeasePayment>(input);
            await _leasePaymentManager.Create(obj);
        }


        public async Task Delete(Guid Id)
        {
            await _leasePaymentManager.Delete(Id);

        }


    }
}
