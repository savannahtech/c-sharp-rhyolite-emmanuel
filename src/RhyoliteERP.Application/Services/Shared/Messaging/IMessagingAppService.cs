using Abp.Application.Services;
using RhyoliteERP.DomainServices.School.Messaging.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Messaging
{
   public interface IMessagingAppService: IApplicationService
    {
        Task Send(StartCampaign startCampaign);
        Task SendPersonalized(StartCampaign startCampaign);

        Task NotifyInvitation(StartCampaign startCampaign);
        Task SendStudentReceipt(ReceiptDto dto);

    }
}
