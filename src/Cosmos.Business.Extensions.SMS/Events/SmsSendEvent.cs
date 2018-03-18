using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Events.Internal;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS.Events {
    public class SmsSendEvent {
        private readonly List<(string nationCode, string phoneNumber)> _phoneNumberList;

        public SmsSendEvent(string phoneNumber, SmsSendEventType eventType, SmsSendEventMoreInfo moreInfo)
            : this(new List<string> {phoneNumber}, eventType, moreInfo) { }

        public SmsSendEvent(List<string> phoneNumbers, SmsSendEventType eventType, SmsSendEventMoreInfo moreInfo) {
            if (phoneNumbers == null) throw new ArgumentNullException(nameof(phoneNumbers));
            if (moreInfo == null) throw new ArgumentNullException(nameof(moreInfo));

            _phoneNumberList = PhoneNumberUtils.CreatePhoneNumberTuple(moreInfo.NationCode, phoneNumbers);

            EventType = eventType;
        }

        public IReadOnlyList<(string nationCode, string phoneNumber)> PhoneNumbers => _phoneNumberList;

        public string CombinedPhoneNumbers => string.Join(",", _phoneNumberList);

        public SmsSendEventType EventType { get; }
    }
}