using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.SendCloud.Models.Results;
using Newtonsoft.Json.Linq;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Core {
    public class SendCloudSendReturnAttribute : JsonReturnAttribute {
        protected override async Task<object> GetTaskResult(ApiActionContext context) {
            var response = context.ResponseMessage;
            var s = await response.Content.ReadAsStringAsync();

            try {
                var json = JObject.Parse(s);
                if (json.Property("statusCode") != null) {
                    return json.ToObject<ResponseData<SmsCalledResult>>();
                }

                return new ResponseData<SmsCalledResult> {
                    StatusCode = 500,
                    Message = s
                };
            }
            catch (Exception e) {
                ExceptionHandleResolver.ResolveHandler()?.Invoke(e);
            }

            return new ResponseData<SmsCalledResult> {
                StatusCode = (int) response.StatusCode,
                Message = "发送失败",
                Result = false
            };
        }
    }
}