using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models.Results
{
    /// <summary>
    /// Called failure item
    /// documentation:
    ///     http://sendcloud.sohu.com/doc/sms/api/#_1
    /// reference to:
    ///     https://github.com/LonghronShen/SendCloudSDK/blob/netstandard2.0/src/SendCloudSDK/Models/Results/SmsSendFailureItem.cs
    /// </summary>
    public class CalledFailureItem
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("vars")]
        public JObject Vars { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}