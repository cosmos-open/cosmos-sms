using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Configuration;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Core;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Models {
    public class YuntongxunMessage {

        public List<string> PhoneLists { get; set; } = new List<string>();

        public string TemplateId { get; set; }

        public string Message { get; set; }

        public string GetPhoneString() => string.Join(",", PhoneLists);

        public void CheckParameters() {
            if (TemplateId == null) {
                throw new InvalidArgumentException("模版为空", Constants.ServiceName, 401);
            }

            var phoneCount = PhoneLists?.Count;
            if (phoneCount == 0) {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (phoneCount > Constants.MaxReceivers) {
                throw new InvalidArgumentException("收信人超过限制", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Message)) {
                throw new InvalidArgumentException("内容不能为空", Constants.ServiceName, 401);
            }
        }

        public object ToSendObject(YuntongxunAccount account) {
            return new {to = GetPhoneString(), appId = account.AppId, templateId = TemplateId, datas = new List<string> {Message}};
        }
    }
}