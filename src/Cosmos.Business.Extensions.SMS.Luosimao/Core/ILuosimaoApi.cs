using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Luosimao.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.Luosimao.Core
{
    [HttpHost("http://sms-api.luosimao.com")]
    public interface ILuosimaoApi : IHttpApiClient
    {
        [HttpPost("/v1/send.json")]
        [JsonReturn]
        ITask<LuosimaoSendResult> SendAsync([HttpHeaderField(Key = "Authorization")] string auth, FormUrlEncodedContent content);
    }
}