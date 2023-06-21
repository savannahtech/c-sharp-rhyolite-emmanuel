using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.ResidentAccounts;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.ResidentAccounts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.ResidentAccounts
{
    public class ResidentAccountAppService: RhyoliteERPAppServiceBase, IResidentAccountAppService
    {
        private readonly IResidentAccountManager _residentAccountManager;
        private readonly IMapper _mapper;

        public ResidentAccountAppService(IResidentAccountManager residentAccountManager, IMapper mapper)
        {
            _residentAccountManager = residentAccountManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _residentAccountManager.ListAll();
        }

        public async Task Create(CreateResidentAccountInput input)
        {
            var obj = _mapper.Map<ResidentAccount>(input);
            await _residentAccountManager.Create(obj);
        }

    }
}
