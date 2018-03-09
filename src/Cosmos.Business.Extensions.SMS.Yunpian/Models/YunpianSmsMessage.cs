using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Yunpian.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Yunpian.Models {
    public class YunpianSmsMessage : ISMSMessage {
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public string Content { get; set; }
        public string Extend { get; set; }

        public string GetPhoneString() => string.Join(",", PhoneNumbers);

        public void CheckParameters() {
            var phoneCount = PhoneNumbers?.Count;
            if (phoneCount == 0) {
                throw new YunpianSmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(Content)) {
                throw new YunpianSmsException("信息为空");
            }
        }
    }
}