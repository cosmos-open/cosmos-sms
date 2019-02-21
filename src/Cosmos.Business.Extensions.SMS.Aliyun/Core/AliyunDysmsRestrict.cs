using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core
{
    public sealed class AliyunDysmsRestrict : SmsSendRestrictTemplate
    {
        public static AliyunDysmsRestrict Instance { get; } = new AliyunDysmsRestrict();

        private static Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules
            = new Dictionary<SmsSendEventType, SmsSendEventRule> {
                {SmsSendEventType.Code, new SmsSendEventRule(AliyunDysmsConstants.MaxReceivers)},
                {SmsSendEventType.Message, new SmsSendEventRule(AliyunDysmsConstants.MaxReceivers)}
            };

        private AliyunDysmsRestrict() : base(_legitimateSmsSendEventRules) { }
    }
}