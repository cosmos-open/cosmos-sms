using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core {
    public sealed class ChuangLanRestrict : SmsSendRestrictTemplate {
        private static readonly ChuangLanRestrict _selfCache = new ChuangLanRestrict();
        public static ChuangLanRestrict Instance => _selfCache;

        private static Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules
            = new Dictionary<SmsSendEventType, SmsSendEventRule> {
                {SmsSendEventType.Code, new SmsSendEventRule(1)}
            };

        private ChuangLanRestrict() : base(_legitimateSmsSendEventRules) { }
    }
}