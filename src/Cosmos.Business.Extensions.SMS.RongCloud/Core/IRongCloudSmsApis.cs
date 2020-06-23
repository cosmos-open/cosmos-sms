using System.Net.Http;
using Cosmos.Business.Extensions.SMS.RongCloud.Models;
using Cosmos.Business.Extensions.SMS.RongCloud.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Core
{
    [HttpHost("http://api.sms.ronghub.com")]
    public interface IRongCloudSmsApis : IHttpApi
    {
        [HttpPost("/sendNotify.json")]
        [JsonReturn]
        ITask<RongCloudSmsResult> SendSmsAsync([SignatureBag] RongCloudSignatureBag bag, FormUrlEncodedContent content);
    }
}