using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.TencentCloud.Exceptions;

namespace Cosmos.Business.Extensions.SMS.TencentCloud.Models {
    public class TencentSmsSendMessage {
        public List<string> PhoneNumbers { get; set; }

        public string NationCode { get; set; } = "86";

        public string Content { get; set; }

        public void CheckParameters() {
            var phoneCount = PhoneNumbers?.Count;
            if (phoneCount == 0) {
                throw new TencentSmsException("收信人为空");
            }
        }
    }
}