using Cosmos.Business.Extensions.SMS.ChuangLan.Exceptions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models
{
    public class ChuangLanSmsCode: ChuangLanSmsBase
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        public void CheckParameters()
        {
            if (string.IsNullOrWhiteSpace(Phone))
            {
                throw new ChuangLanSmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(Msg))
            {
                throw new ChuangLanSmsException("验证码为空");
            }
        }
    }
}
