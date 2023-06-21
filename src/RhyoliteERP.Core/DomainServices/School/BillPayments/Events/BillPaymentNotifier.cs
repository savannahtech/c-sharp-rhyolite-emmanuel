using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Microsoft.Extensions.Configuration;
using RhyoliteERP.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillPayments.Events
{
   public class BillPaymentNotifier : IEventHandler<ReceiptData>, ITransientDependency
    {
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly string _receiptQueue;
        private readonly string _topicExchangeType;
        public BillPaymentNotifier(IConfiguration configuration, IRabbitMqClient rabbitMqClient)
        {
            _receiptQueue = configuration["RabbitMqBroker:PaymentReceiptQueue"];
            _topicExchangeType = configuration["RabbitMqBroker:ExchangeTypes:AmqTopic"];
            _rabbitMqClient = rabbitMqClient;
        }

        public  void HandleEvent(ReceiptData receiptData)
        {
            //produce receipt to mq
            _rabbitMqClient.Produce(_topicExchangeType, _receiptQueue, receiptData);

        }
        
    }
}
