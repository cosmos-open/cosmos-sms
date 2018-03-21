using Cosmos.Business.Extensions.SMS.ChuangLan.Core;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models {
    public class ChuangLanSmsCode : ChuangLanSmsBase {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(Phone)) {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Msg)) {
                throw new InvalidArgumentException("验证码为空", Constants.ServiceName, 401);
            }
        }
    }
}