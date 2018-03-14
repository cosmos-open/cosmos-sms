using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.RongCloud.Exceptions;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Models {
    public class RongCloudSmsMessage {
        public string Mobile { get; set; }
        public string TemplateId { get; set; }
        public string Region { get; set; } = "86";
        public Dictionary<int, string> Vars { get; set; } = new Dictionary<int, string>();

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(Mobile)) throw new RongCloudSmsException("Paramer 'mobile' is required");
            if (string.IsNullOrWhiteSpace(TemplateId)) throw new RongCloudSmsException("Paramer 'templateId' is required");
            if (string.IsNullOrWhiteSpace(Region)) throw new RongCloudSmsException("Paramer 'region' is required");
        }
    }
}