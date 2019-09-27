using System;
using System.Net.Http;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core.Helpers
{
    internal static class WebApiClientCreator
    {
        public static IBaiduYunSmsApis Create(string apiUri, int timeout)
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(timeout),
                BaseAddress = new Uri(apiUri)
            };

            var httpConfig = new HttpApiConfig(client);

            return HttpApi.Create<IBaiduYunSmsApis>(httpConfig);
        }
    }
}