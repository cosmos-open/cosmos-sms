using System;
using System.Net.Http;
using Cosmos.Business.Extensions.SMS.ChuangLan.Configuration;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core.Helpers
{
    internal static class WebApiClientCreator
    {
        public static IChuanglanApi Create(ChuanglanConfig config)
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(config.Timeout),
                BaseAddress = new Uri(config.Account.ApiUrl)
            };
            
            var httpConfig = new HttpApiConfig(client);

            return HttpApi.Create<IChuanglanApi>(httpConfig);
        }
    }
}