using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Abstractions;
using Cosmos.Business.Extensions.SMS.SendCloud.Core.Extensions;
using Cosmos.Business.Extensions.SMS.SendCloud.Exceptions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models {
    public class SendCloudSmsMessage : ISMSMessage {
        [JsonProperty("templateId")]
        public int? TemplateId { get; set; }

        [JsonProperty("msgType")]
        public int? MsgType { get; set; } = 0;

        [JsonProperty("phone")]
        public List<String> Phone { get; set; } = new List<string>();

        [JsonProperty("vars")]
        public Dictionary<string, string> Vars { get; set; } = new Dictionary<string, string>();

        public string GetPhoneString() => string.Join(",", Phone);

        public string GetVarsString() => Vars.ToJson();

        public void CheckParameters() {
            if (TemplateId == null) {
                throw new SendCloudSmsException("模版为空");
            }

            var phoneCount = Phone?.Count;
            if (phoneCount == 0) {
                throw new SendCloudSmsException("收信人为空");
            }

            if (phoneCount > Core.Constants.MaxReceivers) {
                throw new SendCloudSmsException("收信人超过限制");
            }
        }
    }
}