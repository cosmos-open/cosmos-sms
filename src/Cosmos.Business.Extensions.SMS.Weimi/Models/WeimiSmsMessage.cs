using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Weimi.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Weimi.Models {
    public class WeimiSmsMessage {

        public string Content { get; set; }

        public List<string> PhoneNumbers { get; set; } = new List<string>();

        public string GetPhoneString() => string.Join(",", PhoneNumbers);

        public string Timing { get; set; }

        public void CheckParameters() {
            var phoneCount = PhoneNumbers?.Count;
            if (phoneCount == 0) {
                throw new WeimiSmsException("收信人为空");
            }

            if (phoneCount > Core.Constants.MaxReceivers) {
                throw new WeimiSmsException("收信人超过限制");
            }

            if (string.IsNullOrWhiteSpace(Content)) {
                throw new WeimiSmsException("内容不能为空");
            }
        }
    }
}