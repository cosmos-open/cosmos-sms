using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Business.Extensions.SMS.Abstractions;
using Cosmos.Business.Extensions.SMS.ChuangLan.Exceptions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models
{
    public class ChuangLanSmsCode:ISMSCode
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        public void CheckParameters()
        {
            if (string.IsNullOrWhiteSpace(this.Phone))
            {
                throw new ChuangLanSmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(this.Msg))
            {
                throw new ChuangLanSmsException("验证码为空");
            }
        }
    }
}
