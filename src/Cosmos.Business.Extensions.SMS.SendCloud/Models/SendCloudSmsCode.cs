using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.SendCloud.Core;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models {
    public class SendCloudSmsCode {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("labelId")]
        public int? LabelId { get; set; }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(this.Phone)) {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(this.Code)) {
                throw new InvalidArgumentException("验证码为空", Constants.ServiceName, 401);
            }
        }
    }
}