using Cosmos.Business.Extensions.SMS.BaiduYun.Core.Attributes;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core
{
    public interface IBaiduYunSmsApis : IHttpApi
    {
        [Header("ContentType", "application/json;charset=utf-8")]
        [HttpPost("/bce/v2/message")]
        [JsonReturn]
        ITask<BaiduYunSmsResult> SendAsync([Bce] BceObjectWrapper wrapper, [JsonContent] object obj);
    }
}