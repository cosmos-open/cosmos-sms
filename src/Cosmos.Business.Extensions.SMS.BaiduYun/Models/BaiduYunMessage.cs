using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.BaiduYun.Configuration;
using Cosmos.Business.Extensions.SMS.BaiduYun.Core;
using Cosmos.Business.Extensions.SMS.Exceptions;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Models {
    public class BaiduYunMessage {
        public string PhoneNumber { get; set; }

        public string TemplateCode { get; set; }

        public Dictionary<string, string> Vars { get; set; } = new Dictionary<string, string>();


        public string GetVarsString() => Vars.ToJson();

        public virtual void CheckParameters() {
            if (string.IsNullOrWhiteSpace(PhoneNumber)) {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(TemplateCode)) {
                throw new InvalidArgumentException("短信模板 ID 不能为空", Constants.ServiceName, 401);
            }
        }

        public object ToSendObject(BaiduYunConfig config) {
            if (Vars != null && Vars.Any()) {
                return new {
                    invokeId = config.InvokeId,
                    phoneNumber = PhoneNumber,
                    templateCode = TemplateCode,
                    contentVar = Vars
                };
            } else {
                return new {
                    invokeId = config.InvokeId,
                    phoneNumber = PhoneNumber,
                    templateCode = TemplateCode
                };
            }
        }
    }
}