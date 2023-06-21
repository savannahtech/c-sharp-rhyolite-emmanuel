using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.RentalOwners;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.RentalOwners.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.RentalOwners
{
    public class RentalOwnerAppService: RhyoliteERPAppServiceBase, IRentalOwnerAppService
    {
        private readonly IRentalOwnerManager _rentalOwnerManager;
        private readonly IMapper _mapper;

        public RentalOwnerAppService(IRentalOwnerManager rentalOwnerManager, IMapper mapper)
        {
            _rentalOwnerManager = rentalOwnerManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _rentalOwnerManager.ListAll();
        }

        public async Task Create(CreateRentalOwnerInput input)
        {
            var obj = _mapper.Map<RentalOwner>(input);
            await _rentalOwnerManager.Create(obj);
        }

        public async Task Update(UpdateRentalOwnerInput input)
        {
            var obj = _mapper.Map<RentalOwner>(input);
            await _rentalOwnerManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _rentalOwnerManager.Delete(Id);

        }
    }
}
