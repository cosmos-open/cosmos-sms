using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Business.Extensions.SMS.Events.Restrict {
    public abstract class SmsSendRestrictTemplate {
        private readonly SmsSendEventType[] _legitimateSmsSendEventTypes;
        private readonly Dictionary<SmsSendEventType, SmsSendEventRule> _legitimateSmsSendEventRules;

        protected SmsSendRestrictTemplate(Dictionary<SmsSendEventType, SmsSendEventRule> rules, int maxReceivers) {
            if (rules == null || !rules.Any()) throw new ArgumentNullException(nameof(rules));
            if (maxReceivers <= 0) throw new ArgumentOutOfRangeException(nameof(maxReceivers), maxReceivers, "Max reseivers limited must larger than zero.");
            _legitimateSmsSendEventTypes = rules.Keys.ToArray();
            _legitimateSmsSendEventRules = rules;
            MaxReceivers = maxReceivers;
        }

        public IReadOnlyList<SmsSendEventType> LegitimateSmsSendEventTypes => _legitimateSmsSendEventTypes;

        public IReadOnlyDictionary<SmsSendEventType, SmsSendEventRule> LegitimateSmsSendEventRules => _legitimateSmsSendEventRules;

        public int MaxReceivers { get; }
    }
}