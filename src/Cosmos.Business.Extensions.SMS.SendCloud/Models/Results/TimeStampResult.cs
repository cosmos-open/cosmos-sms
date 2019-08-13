using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models.Results
{
    public class TimeStampResult
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}