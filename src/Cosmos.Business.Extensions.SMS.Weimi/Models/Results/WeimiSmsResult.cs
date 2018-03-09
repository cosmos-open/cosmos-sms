using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.Weimi.Models.Results {
    public class WeimiSmsResult {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        public bool IsSuccess() {
            return Code == 0;
        }
    }
}