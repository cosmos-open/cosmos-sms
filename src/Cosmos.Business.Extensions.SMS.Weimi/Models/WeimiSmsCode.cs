using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.Weimi.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Weimi.Models {
    public class WeimiSmsCode : ISMSCode {

        public string TemplateId { get; set; }

        public List<string> PhoneNumbers { get; set; } = new List<string>();

        public string GetPhoneString() => string.Join(",", PhoneNumbers);

        public Dictionary<int, string> TemplateParams { get; set; } = new Dictionary<int, string>();

        public Dictionary<string, string> GetTemplateParams() => TemplateParams.ToDictionary(k => $"p{k.Key}", v => v.Value);

        public string Timing { get; set; }

        public void CheckParameters() {
            if (TemplateId == null) {
                throw new WeimiSmsException("模版为空");
            }

            var phoneCount = PhoneNumbers?.Count;
            if (phoneCount == 0) {
                throw new WeimiSmsException("收信人为空");
            }

            if (phoneCount > Core.Constants.MaxReceivers) {
                throw new WeimiSmsException("收信人超过限制");
            }
        }
    }
}