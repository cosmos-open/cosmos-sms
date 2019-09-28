using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core
{
    public sealed class ChuanglanRestrict : SmsSendRestrictTemplate
    {
        public static ChuanglanRestrict Instance { get; } = new ChuanglanRestrict();

        private static Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules
            = new Dictionary<SmsSendEventType, SmsSendEventRule> {
                {SmsSendEventType.Code, new SmsSendEventRule(1)}
            };

        private ChuanglanRestrict() : base(_legitimateSmsSendEventRules) { }
    }
}