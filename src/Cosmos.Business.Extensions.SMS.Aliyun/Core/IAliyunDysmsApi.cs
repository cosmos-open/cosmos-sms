using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Aliyun.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core {
    public interface IAliyunDysmsApi : IHttpApiClient {
        [HttpPost]
        [JsonReturn]
        [Header("x-sdk-client", "Net/2.0.0")]
        ITask<AliyunDysmsResult> SendMessageAsync(FormUrlEncodedContent content);

        [HttpPost]
       // [JsonReturn]
        [Header("x-sdk-client", "Net/2.0.0")]
        ITask<AliyunDysmsResult> SendCodeAsync(FormUrlEncodedContent content);
    }
}