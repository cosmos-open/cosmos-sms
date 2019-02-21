using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Huyi.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.Huyi.Core
{
    [HttpHost("http://106.ihuyi.com")]
    public interface IHuyiApis : IHttpApiClient
    {
        [HttpPost("/webservice/sms.php")]
        [JsonReturn]
        ITask<HuyiResult> SendAsync(FormUrlEncodedContent content, string method = "Submit");
    }
}