using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.IrsSignatures;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.IrsSignatures.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.IrsSignatures
{
    public class IrsSignatureAppService: RhyoliteERPAppServiceBase, IIrsSignatureAppService
    {
        private readonly IIrsSignatureManager _irsSignatureManager;
        private readonly IMapper _mapper;

        public IrsSignatureAppService(IIrsSignatureManager irsSignatureManager, IMapper mapper)
        {
            _irsSignatureManager = irsSignatureManager;
            _mapper = mapper;
        }

        public async Task Create(CreateIrsSignatureInput input)
        {
            var obj = _mapper.Map<IrsSignature>(input);
            await _irsSignatureManager.Create(obj);
        }

        public async Task<object> GetSignature()
        {
            return await _irsSignatureManager.GetSignature();
        }

    }
}
