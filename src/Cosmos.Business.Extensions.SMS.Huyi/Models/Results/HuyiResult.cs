using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.Huyi.Models.Results
{
    public class HuyiResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("smsid")]
        public string SmsId { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}