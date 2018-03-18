using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Core.Aggregation;
using Cosmos.Business.Extensions.SMS.Events;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS {
    public abstract partial class SmsSenderBase : ISmsSender, IDisposable {
        private readonly ISmsAggregationSender _smsAggregationSender;
        private readonly List<string> _specificImplementList;
        private static readonly List<string> _emptyImplementList;

        protected SmsSenderBase(
            ISmsAggregationSender smsAggregationSender,
            List<string> specificImplementList = null) {
            _smsAggregationSender = smsAggregationSender ?? throw new ArgumentNullException(nameof(smsAggregationSender));
            _specificImplementList = specificImplementList ?? _emptyImplementList;
        }

        public void SendMessage(string phoneNumber, string message, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumber, SmsSendEventType.Message, moreInfo ?? new SmsSendEventMoreInfo());
            throw new System.NotImplementedException();
        }

        public void SendMessage(List<string> phoneNumbers, string message, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumbers, SmsSendEventType.Message, moreInfo ?? new SmsSendEventMoreInfo());
            throw new System.NotImplementedException();
        }

        public void SendCode(string phoneNumber, string code, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumber, SmsSendEventType.Message, moreInfo ?? new SmsSendEventMoreInfo());
            throw new System.NotImplementedException();
        }

        public void SendCode(List<string> phoneNumbers, string code, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumbers, SmsSendEventType.Message, moreInfo ?? new SmsSendEventMoreInfo());
            throw new System.NotImplementedException();
        }

        public void SendTemplateMessage(string phoneNumber, string templateCode, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumber, SmsSendEventType.Message, moreInfo ?? new SmsSendEventMoreInfo());
            throw new System.NotImplementedException();
        }

        public void SendTemplateMessage(List<string> phoneNumbers, string templateCode, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumbers, SmsSendEventType.Message, moreInfo ?? new SmsSendEventMoreInfo());
            throw new System.NotImplementedException();
        }

        private void Emit(SmsSendEvent sendEvent) {
            Task.Factory.StartNew(async () => await _smsAggregationSender.SendAsync(sendEvent));
        }

        public void Dispose() { }
    }
}