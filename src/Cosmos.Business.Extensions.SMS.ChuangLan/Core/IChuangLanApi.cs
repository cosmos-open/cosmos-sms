using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core
{
    public interface IChuangLanApi:IHttpApiClient
    {
        [HttpPost("/msg/send/json")]       
        ITask<ResponseData> SendMessageAsync(FormUrlEncodedContent content);
        
        [HttpPost("/msg/send/json")]
        [ChuangLanSendReturn]
        ITask<ResponseData> SendCodeAsync([JsonContent]IDictionary<string,string> content);
    }
}
