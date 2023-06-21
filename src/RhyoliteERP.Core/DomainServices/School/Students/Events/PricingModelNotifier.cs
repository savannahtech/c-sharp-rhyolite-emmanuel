using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Microsoft.Extensions.Configuration;
using RhyoliteERP.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Students.Events
{
   public class PricingModelNotifier : IEventHandler<PricingModelData>, ITransientDependency
    {
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly string _smPricingModelQueue;
        private readonly string _topicExchangeType;


        public PricingModelNotifier(IConfiguration configuration, IRabbitMqClient rabbitMqClient)
        {
            _smPricingModelQueue = configuration["RabbitMqBroker:SchoolManagerPricingModel"];
            _topicExchangeType = configuration["RabbitMqBroker:ExchangeTypes:AmqDirect"];
            _rabbitMqClient = rabbitMqClient;
        }

        public void HandleEvent(PricingModelData pricingModelData)
        {
            _rabbitMqClient.Produce(_topicExchangeType, _smPricingModelQueue, pricingModelData);

        }
    }
}
