using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Weimi.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.Weimi.Core
{
    public interface IWeimiSmsApi : IHttpApiClient
    {
        [HttpGet("/2/sms/send.html")]
        [JsonReturn]
        ITask<WeimiSmsResult> SendMessageAsync(FormUrlEncodedContent content);

        [HttpGet("/2/sms/send.html")]
        [JsonReturn]
        ITask<WeimiSmsResult> SendCodeAsync(FormUrlEncodedContent content);
    }
}