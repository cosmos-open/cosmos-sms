using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.SendCloud.Core;
using Cosmos.Business.Extensions.SMS.SendCloud.Core.Extensions;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models {
    public class SendCloudSmsMessage {

        public int? TemplateId { get; set; }

        public int? MsgType { get; set; } = 0;

        public List<string> Phone { get; set; } = new List<string>();

        public Dictionary<string, string> Vars { get; set; } = new Dictionary<string, string>();

        public string GetPhoneString() => string.Join(",", Phone);

        public string GetVarsString() => Vars.ToJson();

        public void CheckParameters() {
            if (TemplateId == null) {
                throw new InvalidArgumentException("模版为空", Constants.ServiceName, 401);
            }

            var phoneCount = Phone?.Count;
            if (phoneCount == 0) {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (phoneCount > Core.Constants.MaxReceivers) {
                throw new InvalidArgumentException("收信人超过限制", Constants.ServiceName, 401);
            }
        }
    }
}