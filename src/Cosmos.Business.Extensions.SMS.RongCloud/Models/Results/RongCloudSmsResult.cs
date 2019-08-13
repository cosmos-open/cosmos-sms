using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Models.Results
{
    public class RongCloudSmsResult
    {
        // 返回码，200 为正常。
        [JsonProperty("code")]
        public int Code { get; set; }

        // 短信验证码唯一标识。
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        // 错误信息。
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}