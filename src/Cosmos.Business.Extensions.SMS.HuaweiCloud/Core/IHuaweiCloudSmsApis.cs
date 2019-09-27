using System.Net.Http;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Attributes;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core
{
    [Header("Authorization","WSSE realm=\"SDP\",profile=\"UsernameToken\",type=\"Appkey\"")]
    public interface IHuaweiCloudSmsApis : IHttpApi
    {
        [HttpPost("/sms/batchSendSms/v1")]
        [JsonReturn]
        ITask<HuaweiCloudSmsResult> SendMessageAsync(FormUrlEncodedContent content, [Wsse] WsseObject account);

        [HttpPost("/sms/batchSendSms/v1")]
        [JsonReturn]
        ITask<HuaweiCloudSmsResult> SendCodeAsync(FormUrlEncodedContent content, [Wsse] WsseObject account);
    }
}