using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.JdCloud.Configuration;
using Cosmos.Business.Extensions.SMS.JdCloud.Models.Results;
using JDCloudSDK.Sms.Apis;
using JDCloudSDK.Sms.Client;
using Polly;

namespace Cosmos.Business.Extensions.SMS.JdCloud.Core
{
    internal static class SmsCoreSender
    {
        public static async Task<JdCloudSmsResult> SendAsync(SmsClient client, BatchSendRequest request, JdCloudSmsConfig config)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var asyncPolicy = Policy.HandleResult<JdCloudSmsResult>(r => !r.IsSuccess())
                .RetryAsync(config.RetryTimes);

            return await asyncPolicy.ExecuteAsync(() => CoreProcessAsync(client, request));
        }

        private static async Task<JdCloudSmsResult> CoreProcessAsync(SmsClient client, BatchSendRequest request)
        {
            var response = await client.BatchSend(request);

            return new JdCloudSmsResult(response);
        }
    }
}