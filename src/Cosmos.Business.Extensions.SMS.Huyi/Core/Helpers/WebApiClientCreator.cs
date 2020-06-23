using System;
using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Huyi.Configuration;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.Huyi.Core.Helpers
{
    internal static class WebApiClientCreator
    {
        public static IHuyiApis Create(HuyiConfig config)
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(config.Timeout)
            };

            return HttpApi.Create<IHuyiApis>(new HttpApiConfig(client));
        }
    }
}