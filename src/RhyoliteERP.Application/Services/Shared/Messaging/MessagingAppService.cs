using RhyoliteERP.DomainServices.School.Messaging;
using RhyoliteERP.DomainServices.School.Messaging.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Messaging
{
   public class MessagingAppService: RhyoliteERPAppServiceBase, IMessagingAppService
    {
        private readonly IMessagingManager _messagingManager;

        public MessagingAppService(IMessagingManager messagingManager)
        {
            _messagingManager = messagingManager;
        }

        public async Task Send(StartCampaign startCampaign)
        {
            //
            await _messagingManager.Send(startCampaign);
        }

        public async Task SendPersonalized(StartCampaign startCampaign)
        {
            await _messagingManager.SendPersonalized(startCampaign);
        }

        public async Task NotifyInvitation(StartCampaign startCampaign)
        {
            await _messagingManager.NotifyInvitation(startCampaign);
        }


        public async Task SendStudentReceipt(ReceiptDto dto)
        {
            await _messagingManager.SendReceipt(dto);
        }
    }
}
