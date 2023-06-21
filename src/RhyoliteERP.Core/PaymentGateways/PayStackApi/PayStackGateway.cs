using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.PaymentGateways.PayStackApi.Dto;
using RhyoliteERP.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RhyoliteERP.PaymentGateways.PayStackApi
{
   public class PayStackGateway : Abp.Domain.Services.DomainService, IPayStackGateway
    {

        private readonly IRepository<CompanyProfile, Guid> _repositoryCompanyProfile;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly string _baseUrl;
        private readonly string _liveSecretKey;
        private readonly string _callBackUrl;
        private readonly string _callBackApiBaseUrl;
        private readonly int _paymentRequestDatabaseId;

        public PayStackGateway(IRepository<CompanyProfile, Guid> repositoryCompanyProfile, IConfiguration configuration, IRedisCacheManager redisCacheManager)
        {
            _repositoryCompanyProfile = repositoryCompanyProfile;
            _baseUrl = configuration["PayStackApi:BaseUrl"];
            _liveSecretKey = configuration["PayStackApi:LiveSecretKey"];
            _callBackUrl = configuration["PayStackApi:CallBackUrl"];
            _callBackApiBaseUrl = configuration["PayStackApi:CallBackApiBaseUrl"];
            _paymentRequestDatabaseId = Convert.ToInt32(configuration["RedisCache:PaymentRequestDatabaseId"]);
            _redisCacheManager = redisCacheManager;
        }

        public async Task<InitializePayResponse> Initialize(decimal amt, string clientReference,int? tenantId,string serviceType)
        {
            //drop keys in redis
            var paymentRequest = new { 
                PaymentGateway = "paystack", 
                serviceType, 
                AccountSource = "erp", 
                tenantId, 
                clientReference, 
                amount = amt, 
                callBackApiUrl = $"{_callBackApiBaseUrl}PaymentRequest/Finalize/?tenantId={tenantId}&accountSource=erp&serviceType={serviceType}", 
                //returnUrl = "http://localhost:5200/Manage/Subscription"
                returnUrl = "http://erp.adinkracreation.com/Manage/Subscription"
            };
            
            await _redisCacheManager.SetValueAsync(_paymentRequestDatabaseId,clientReference, JsonConvert.SerializeObject(paymentRequest));
            var cacheTtl = DateTime.UtcNow.AddDays(3) - DateTime.UtcNow;
            await _redisCacheManager.SetExpireTimeAsync(_paymentRequestDatabaseId, clientReference, cacheTtl);
            var companyProfiile = _repositoryCompanyProfile.GetAll().FirstOrDefault();

            var client = new RestClient($"{_baseUrl}initialize");

            var request = new RestRequest()
            //request.AddHeader("Authorization", $"Bearer {_testSecretKey}");
            .AddHeader("Authorization", $"Bearer {_liveSecretKey}")
            .AddHeader("Content-Type", "application/json");

            if (companyProfiile != null)
            {
                var reqBody = new
                {
                    email = $"{companyProfiile.PhoneNo}@adinkracreation.com",
                    amount = (int)(amt * 100),
                    reference = clientReference,
                    callback_url = _callBackUrl
                };
                request.AddJsonBody(reqBody);
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(60000);

                var response = await client.PostAsync<InitializePayResponse>(request, cancellationTokenSource.Token);
                return await Task.FromResult(response);

            }
            else
            {
                int p1 = new Random().Next(10000, 99999);
                int p2 = new Random().Next(10000, 99999);

                var reqBody = new
                {
                    email = $"{p1}{p2}@adinkracreation.com",
                    amount = (int)(amt * 100),
                    reference = clientReference,
                    callback_url = _callBackUrl
                };
                request.AddJsonBody(reqBody);
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(60000);

                var response = await client.PostAsync<InitializePayResponse>(request, cancellationTokenSource.Token);
                return await Task.FromResult(response);
            }
        }

        public async Task<VerifyPayResponse> Verify(string reference)
        {
            var client = new RestClient($"{_baseUrl}verify/{reference}");

            var request = new RestRequest()
            //request.AddHeader("Authorization", $"Bearer {_testSecretKey}");
            .AddHeader("Authorization", $"Bearer {_liveSecretKey}");

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(60000);

            var response = await client.ExecuteAsync<object>(request, cancellationTokenSource.Token);
            var result = JsonConvert.DeserializeObject<VerifyPayResponse>(response.Content, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });

            return await Task.FromResult(result);
        }


        public async Task<bool> TriggerCallBack(string uri)
        {
            var client = new RestClient(uri.Trim());
            var request = new RestRequest();

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(80000);

            var response = await client.ExecuteAsync<object>(request, cancellationTokenSource.Token);
            
            Logger.Info($"Results from Wallet Api => {JsonConvert.SerializeObject(response.Data)}");

            return response.IsSuccessful;


        }
    }
}
