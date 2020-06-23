using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.JdCloud.Core
{
    public sealed class JdCloudSmsRestrict : SmsSendRestrictTemplate
    {
        public static JdCloudSmsRestrict Instance { get; } = new JdCloudSmsRestrict();

        private static Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules
            = new Dictionary<SmsSendEventType, SmsSendEventRule> {
                {SmsSendEventType.Code, new SmsSendEventRule(JdCloudSmsConstants.MaxReceivers)},
                {SmsSendEventType.Message, new SmsSendEventRule(JdCloudSmsConstants.MaxReceivers)}
            };

        private JdCloudSmsRestrict() : base(_legitimateSmsSendEventRules) { }
    }
}