using System;
using System.Net.Http;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Configuration;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Helpers
{
    internal static class WebApiClientCreator
    {
        public static IHuaweiCloudSmsApis Create(HuaweiCloudSmsConfig config)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromMilliseconds(config.TimeOut);
            client.BaseAddress = new Uri(ApiAddressHelper.GetGateway(config.RegionName));

            var httpConfig = new HttpApiConfig(client);

            return HttpApi.Create<IHuaweiCloudSmsApis>(httpConfig);
        }
    }
}