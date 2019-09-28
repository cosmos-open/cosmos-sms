using System.Net.Http;
using Cosmos.Business.Extensions.SMS.SendCloud.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Core
{
    [HttpHost("http://www.sendcloud.net")]
    public interface ISendCloudApi : IHttpApi
    {
        [HttpGet("/smsapi/timestamp/get")]
        [TimeStampReturn]
        ITask<ResponseData<TimeStampResult>> GetTimeStampAsync();

        [HttpPost("/smsapi/send")]
        [SendCloudSendReturn]
        ITask<ResponseData<SmsCalledResult>> SendMessageAsync(FormUrlEncodedContent content);

        [HttpPost("/smsapi/send")]
        [SendCloudSendReturn]
        ITask<ResponseData<SmsCalledResult>> SendCodeAsync(FormUrlEncodedContent content);
    }
}