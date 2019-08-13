using System.Net.Http;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Core
{
    public interface IKsyunSmsApis : IHttpApiClient
    {
        [HttpPost]
        [JsonReturn]
        ITask<KsyunSendResult> SendAsync([AwsV4Header] AwsV4.SignedResult signedResult, FormUrlEncodedContent content);
    }
}