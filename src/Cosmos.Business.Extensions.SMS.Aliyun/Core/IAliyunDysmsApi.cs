using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Aliyun.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core {
    public interface IAliyunDysmsApi : IHttpApiClient {
        [HttpPost]
        [JsonReturn]
        ITask<AliyunDysmsResult> SendMessageAsync(FormUrlEncodedContent content);

        [HttpPost]
        [JsonReturn]
        ITask<AliyunDysmsResult> SendCodeAsync(FormUrlEncodedContent content);
    }
}