using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.RongCloud.Core;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Models {
    public class RongCloudSmsMessage {
        public string Mobile { get; set; }
        public string TemplateId { get; set; }
        public string Region { get; set; } = "86";
        public Dictionary<int, string> Vars { get; set; } = new Dictionary<int, string>();

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(Mobile)) throw new InvalidArgumentException("Paramer 'mobile' is required", Constants.ServiceName, 401);
            if (string.IsNullOrWhiteSpace(TemplateId)) throw new InvalidArgumentException("Paramer 'templateId' is required", Constants.ServiceName, 401);
            if (string.IsNullOrWhiteSpace(Region)) throw new InvalidArgumentException("Paramer 'region' is required", Constants.ServiceName, 401);
        }
    }
}