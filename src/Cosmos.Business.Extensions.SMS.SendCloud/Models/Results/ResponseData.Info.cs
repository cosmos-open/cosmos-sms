using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models.Results
{
    /// <summary>
    /// Api called result
    /// documentation:
    ///     http://sendcloud.sohu.com/doc/sms/api/#_1
    /// reference to:
    ///     https://github.com/LonghronShen/SendCloudSDK/blob/netstandard2.0/src/SendCloudSDK/Models/Results/SendSmsResult.cs
    /// </summary>
    public class SmsCalledResult
    {
        [JsonProperty("successCount")]
        public int SuccessCount { get; set; }

        [JsonProperty("failedCount")]
        public int FailedCount { get; set; }

        [JsonProperty("items")]
        public CalledFailureItem[] Items { get; set; }

        [JsonProperty("smsIds")]
        public string[] SmsIds { get; set; }
    }
}