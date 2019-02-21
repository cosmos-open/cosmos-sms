using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core
{
    public interface IChuangLanApi : IHttpApiClient
    {
        [HttpPost("/msg/send/json")]
        [ChuangLanSendReturn]
        ITask<ResponseData> SendMessageAsync([JsonContent]IDictionary<string, string> content);

        [HttpPost("/msg/variable/json")]
        [ChuangLanSendVariableReturn]
        ITask<VariableResponseData> SendVariableMessageAsync([JsonContent] IDictionary<string, string> content);

        [HttpPost("/msg/send/json")]
        [ChuangLanSendReturn]
        ITask<ResponseData> SendCodeAsync([JsonContent]IDictionary<string, string> content);
    }
}
