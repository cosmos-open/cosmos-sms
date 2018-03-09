using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.Events {
    public class SmsSendEvent {
        private readonly List<string> _phoneNumberList;

        public SmsSendEvent(List<string> phoneNumbers, SmsSendEventType eventType) {
            _phoneNumberList = phoneNumbers ?? throw new ArgumentNullException(nameof(phoneNumbers));
            EventType = eventType;
        }

        public IReadOnlyList<string> PhoneNumbers => _phoneNumberList;

        public string CombinedPhoneNumbers => string.Join(",", _phoneNumberList);

        public SmsSendEventType EventType { get; }
    }
}