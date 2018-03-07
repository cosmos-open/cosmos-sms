using Cosmos.Business.Extensions.SMS.SendCloud.Core.Extensions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models.Results {
    public class ResponseData<TData> {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("info")]
        public TData Info { get; set; }

        public string ToJsonString() {
            return this.ToJson();
        }
    }
}