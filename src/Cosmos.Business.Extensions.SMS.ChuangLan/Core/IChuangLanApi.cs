using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.ChuangLan.Core.Attributes;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core
{
    public interface IChuanglanApi : IHttpApi
    {
        [HttpPost("/msg/send/json")]
        [ChuanglanSendReturn]
        ITask<ResponseData> SendMessageAsync([JsonContent] IDictionary<string, string> content);

        [HttpPost("/msg/variable/json")]
        [ChuanglanSendVariableReturn]
        ITask<VariableResponseData> SendVariableMessageAsync([JsonContent] IDictionary<string, string> content);

        [HttpPost("/msg/send/json")]
        [ChuanglanSendReturn]
        ITask<ResponseData> SendCodeAsync([JsonContent] IDictionary<string, string> content);
    }
}