using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RhyoliteERP.DomainServices.School.Messaging.Dto;
using RhyoliteERP.DomainServices.Shared.Messaging;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Messaging
{
   public class MessagingManager: Abp.Domain.Services.DomainService, IMessagingManager
    {
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IRepository<CompanyProfile, Guid> _repositoryCompanyProfile;
        private readonly IMessageDataPreprocessorManager _messageDataPreprocessorManager;
        private readonly string _messagingQueue;
        private readonly string _topicExchangeType;
        private readonly string _smsStorageCallBackUrl;
        private readonly string _rhyolitePrimeBotToken;
        private readonly List<string> _telegramSupportPersonnel;
        public MessagingManager(IConfiguration configuration, IRabbitMqClient rabbitMqClient, IRepository<CompanyProfile, Guid> repositoryCompanyProfile, IMessageDataPreprocessorManager messageDataPreprocessorManager)
        {
            _messagingQueue = configuration["RabbitMqBroker:MessagingQueue"];
            _topicExchangeType = configuration["RabbitMqBroker:ExchangeTypes:AmqTopic"];
            _smsStorageCallBackUrl = configuration["ReportingApi:SmsStorageCallBackUrl"];
            _rhyolitePrimeBotToken = configuration["BotTokens:RhyolitePrime"];
            _telegramSupportPersonnel = configuration.GetSection("TelegramSupportPersonnel").Get<List<string>>();
            _rabbitMqClient = rabbitMqClient;
            _repositoryCompanyProfile = repositoryCompanyProfile;
            _messageDataPreprocessorManager = messageDataPreprocessorManager;
        }

        public async Task Send(StartCampaign startCampaign)
        {
            var companyProfile = await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();

            var messageList = await _messageDataPreprocessorManager.DoPreprocess(startCampaign.Message, startCampaign.RecipientId, startCampaign.RecipientType);

            if (messageList != null && messageList.Any() && companyProfile!=null)
            {
                //Logger.Info(JsonConvert.SerializeObject(new MessengerModel
                //{
                //    MessagingChannel = startCampaign.MessagingChannel,
                //    AccountSource = "erp", 
                //    ModuleSource = startCampaign.ModuleSource,
                //    TenantId = startCampaign.TenantId,
                //    SmsInfo = new { MessageList = messageList, MessageContent = startCampaign.Message, SmsStorageCallBackUrl = _smsStorageCallBackUrl, companyProfile.ClientId, companyProfile.ClientSecret, companyProfile.SenderId, AccountSource = "erp", startCampaign.TenantId }
                //}));

                _rabbitMqClient.Produce(_topicExchangeType, _messagingQueue,
                    new MessengerModel
                    {
                        MessagingChannel = startCampaign.MessagingChannel,
                        AccountSource = "erp",
                        ModuleSource = startCampaign.ModuleSource,
                        TenantId = startCampaign.TenantId,
                        SmsInfo = new { MessageList = messageList, MessageContent = startCampaign.Message, SmsStorageCallBackUrl = _smsStorageCallBackUrl, companyProfile.ClientId, companyProfile.ClientSecret, companyProfile.SenderId, AccountSource = "erp", startCampaign.TenantId }
                    });

            }

        } 
    
        public async Task SendReceipt(ReceiptDto dto)
        {
            var companyProfile = await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();

            var messageList = await _messageDataPreprocessorManager.DoProcessStudentReceipt(dto.Id, dto.StatementId);

            if (messageList != null && messageList.Any() && companyProfile != null)
            {
                Logger.Info(JsonConvert.SerializeObject(new MessengerModel
                {
                    MessagingChannel = dto.MessagingChannel,
                    AccountSource = "erp",
                    TenantId = dto.TenantId,
                    SmsInfo = new { MessageList = messageList, MessageContent = string.Empty, SmsStorageCallBackBaseUrl = "https://erp.adinkracreation.com/", companyProfile.ClientId, companyProfile.ClientSecret, companyProfile.SenderId, AccountSource = "erp", dto.TenantId }
                }));


                _rabbitMqClient.Produce(_topicExchangeType, _messagingQueue,
                    new MessengerModel
                    {
                        MessagingChannel = dto.MessagingChannel,
                        AccountSource = "erp",
                        TenantId = dto.TenantId,
                        SmsInfo = new { MessageList = messageList, MessageContent = string.Empty, SmsStorageCallBackBaseUrl = "https://erp.adinkracreation.com/", companyProfile.ClientId, companyProfile.ClientSecret, companyProfile.SenderId, AccountSource = "erp", dto.TenantId }
                    });

            }
        }

        public async Task SendPersonalized(StartCampaign startCampaign)
        {
            var companyProfile = await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();

            var messageList = await _messageDataPreprocessorManager.PersonalizedMessagePreprocessor(startCampaign.Message, startCampaign.RecipientList);

            if (messageList != null && messageList.Any() && companyProfile != null)
            {

                _rabbitMqClient.Produce(_topicExchangeType, _messagingQueue,
                    new MessengerModel
                    {
                        MessagingChannel = startCampaign.MessagingChannel,
                        AccountSource = "erp",
                        ModuleSource = startCampaign.ModuleSource,
                        TenantId = startCampaign.TenantId,
                        SmsInfo = new { MessageList = messageList, MessageContent = startCampaign.Message, SmsStorageCallBackUrl = _smsStorageCallBackUrl, companyProfile.ClientId, companyProfile.ClientSecret, companyProfile.SenderId, AccountSource = "erp", startCampaign.TenantId }
                    });
            }

        }
    
        public async Task NotifyInvitation(StartCampaign startCampaign)
        {
            List<object> messageList = new List<object>();

            foreach (var personnel in _telegramSupportPersonnel)
            {
                messageList.Add(new { chatId = personnel, message = startCampaign.Message });

            }

            _rabbitMqClient.Produce(_topicExchangeType, _messagingQueue,
                    new MessengerModel
                    {
                        MessagingChannel = startCampaign.MessagingChannel,
                        AccountSource = "erp",
                        ModuleSource = startCampaign.ModuleSource,
                        TenantId = startCampaign.TenantId,
                        TelegramInfo = new { startCampaign.Message , BotToken = _rhyolitePrimeBotToken , AccountSource = "erp", StorageCallBackUrl = "", startCampaign.TenantId, MessageList = messageList },

                    });

        }
        
    }
}
