using Abp.Domain.Services;
using RhyoliteERP.PaymentGateways.PayStackApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.PaymentGateways.PayStackApi
{
   public interface IPayStackGateway: IDomainService
    {
        Task<InitializePayResponse> Initialize(decimal amt, string clientReference, int? tenantId, string serviceType);
        Task<VerifyPayResponse> Verify(string reference);
        Task<bool> TriggerCallBack(string uri);

    }
}
