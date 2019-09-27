using System;
using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Aliyun.Configuration;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core.Helpers
{
    internal static class WebApiClientCreator
    {
        public static IAliyunDysmsApi Create(AliyunDysmsConfig config)
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(config.TimeOut),
                BaseAddress = config.Security
                    ? new Uri("https://dysmsapi.aliyuncs.com")
                    : new Uri("http://dysmsapi.aliyuncs.com")
            };

            var httpConfig = new HttpApiConfig(client);

            return HttpApi.Create<IAliyunDysmsApi>(httpConfig);
        }
    }
}