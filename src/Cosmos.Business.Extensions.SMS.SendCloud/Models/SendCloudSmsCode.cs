using Cosmos.Business.Extensions.SMS.Abstractions;
using Cosmos.Business.Extensions.SMS.SendCloud.Exceptions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models {
    public class SendCloudSmsCode : ISMSCode {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("labelId")]
        public int? LabelId { get; set; }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(this.Phone)) {
                throw new SendCloudSmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(this.Code)) {
                throw new SendCloudSmsException("验证码为空");
            }
        }
    }
}