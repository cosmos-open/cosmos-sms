using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core {
    public sealed class AliyunDysmsRestrict : SmsSendRestrictTemplate {
        private static readonly AliyunDysmsRestrict _selfCache = new AliyunDysmsRestrict();
        public static AliyunDysmsRestrict Instance => _selfCache;

        private static Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules
            = new Dictionary<SmsSendEventType, SmsSendEventRule> {
                {SmsSendEventType.Code, new SmsSendEventRule(Constants.MaxReceivers)},
                {SmsSendEventType.Message, new SmsSendEventRule(Constants.MaxReceivers)}
            };

        private AliyunDysmsRestrict() : base(_legitimateSmsSendEventRules) { }
    }
}