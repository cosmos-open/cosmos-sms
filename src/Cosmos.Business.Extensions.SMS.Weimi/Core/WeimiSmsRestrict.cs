using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.Weimi.Core
{
    public class WeimiSmsRestrict : SmsSendRestrictTemplate
    {
        private static readonly WeimiSmsRestrict _selfCache = new WeimiSmsRestrict();
        public static WeimiSmsRestrict Instance => _selfCache;

        private static Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules
            = new Dictionary<SmsSendEventType, SmsSendEventRule> {
                {SmsSendEventType.Code, new SmsSendEventRule(Constants.MaxReceivers)},
                {SmsSendEventType.Message, new SmsSendEventRule(Constants.MaxReceivers)}
            };

        private WeimiSmsRestrict() : base(_legitimateSmsSendEventRules) { }
    }
}