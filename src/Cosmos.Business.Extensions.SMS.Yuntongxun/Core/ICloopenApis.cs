using Cosmos.Business.Extensions.SMS.Yuntongxun.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Core {
    public interface ICloopenApis : IHttpApiClient {
        [HttpPost]
        [JsonReturn]
        [AppendHttpHeader]
        ITask<YuntongxunSmsResult> SendAsync([DynamicHttpPost] string path, [Authorization] string authorization, [JsonContent] object obj);
    }
}