using Cosmos.Business.Extensions.SMS.BaiduYun.Models;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core
{
    public interface IBaiduYunSmsApis : IHttpApiClient
    {
        [HttpPost("/bce/v2/message")]
        [JsonReturn]
        [AppendHeader]
        ITask<BaiduYunSmsResult> SendAsync([Bce] BceObjectWrapper wrapper, [JsonContent] object obj);
    }
}