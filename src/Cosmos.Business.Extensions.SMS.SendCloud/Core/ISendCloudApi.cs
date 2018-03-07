using System.Net.Http;
using Cosmos.Business.Extensions.SMS.SendCloud.Models.Results;
using WebApiClient;
using WebApiClient.Attributes;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Core {
    [HttpHost("http://www.sendcloud.net/smsapi")]
    public interface ISendCloudApi : IHttpApiClient {
        [HttpGet("/timestamp/get")]
        [JsonReturn]
        ITask<ResponseData<TimeStampResult>> GetTimeStampAsync();

        [HttpPost("/send")]
        [SendCloudSendReturn]
        ITask<ResponseData<SmsCalledResult>> SendMessageAsync(FormUrlEncodedContent content);

        [HttpPost("/send")]
        [SendCloudSendReturn]
        ITask<ResponseData<SmsCalledResult>> SendCodeAsync(FormUrlEncodedContent content);
    }
}