using RhyoliteERP.DomainServices.School.Messaging.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Messaging
{
   public interface IMessagingManager: Abp.Domain.Services.IDomainService
    {
        Task Send(StartCampaign startCampaign);
        Task SendPersonalized(StartCampaign startCampaign);
        Task NotifyInvitation(StartCampaign startCampaign);
        Task SendReceipt(ReceiptDto dto);
    }
}
