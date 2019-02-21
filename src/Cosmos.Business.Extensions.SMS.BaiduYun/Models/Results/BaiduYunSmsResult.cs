using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Models.Results
{
    public class BaiduYunSmsResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 服务生成的流水号
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestID { get; set; }
    }
}