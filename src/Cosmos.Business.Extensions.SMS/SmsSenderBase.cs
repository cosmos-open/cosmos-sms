using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Core.Aggregation;
using Cosmos.Business.Extensions.SMS.Events;
using Cosmos.Business.Extensions.SMS.Events.Restrict;

namespace Cosmos.Business.Extensions.SMS {
    public abstract class SmsSenderBase : ISmsSender, IDisposable {
        private readonly ISmsAggregationSender _smsAggregationSender;
        private readonly List<string> _specificImplementList;
        private static readonly List<string> _emptyImplementList = new List<string>();
        private readonly SmsBizPkgConfiguration _businessPackageConfiguration;

        protected SmsSenderBase(
            ISmsAggregationSender smsAggregationSender,
            SmsBizPkgConfiguration smsBizPkgConfiguration,
            List<string> specificImplementList = null) {
            _smsAggregationSender = smsAggregationSender ?? throw new ArgumentNullException(nameof(smsAggregationSender));
            _businessPackageConfiguration = smsBizPkgConfiguration ?? throw new ArgumentNullException(nameof(smsBizPkgConfiguration));
            _specificImplementList = MergeSpecificImplementList(specificImplementList, _businessPackageConfiguration.GlobalSpecificImplementList);
        }

        private List<string> MergeSpecificImplementList(List<string> givenList, IReadOnlyList<string> globalList) {
            if (globalList == null || !globalList.Any()) {
                return givenList ?? _emptyImplementList;
            }

            var globalGivenList = globalList.ToList();
            if (givenList == null || !givenList.Any()) {
                return globalGivenList ?? _emptyImplementList;
            }

            return givenList.Where(x => globalGivenList.Contains(x)).ToList();
        }

        public void SendMessage(string phoneNumber, string message, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumber, SmsSendEventType.Message, GetLegalMoreInfo(moreInfo));

            Emit(sendEvent);
        }

        public void SendMessage(string phoneNumber, string message, List<string> serviceNames, SmsSendEventMoreInfo moreInfo = null) {
            SendMessage(phoneNumber, message, GetLegalMoreInfo(moreInfo, serviceNames));
        }

        public void SendMessage(List<string> phoneNumbers, string message, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumbers, SmsSendEventType.Message, GetLegalMoreInfo(moreInfo));

            Emit(sendEvent);
        }

        public void SendMessage(List<string> phoneNumbers, string message, List<string> serviceNames, SmsSendEventMoreInfo moreInfo = null) {
            SendMessage(phoneNumbers, message, GetLegalMoreInfo(moreInfo, serviceNames));
        }

        public void SendCode(string phoneNumber, string code, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumber, SmsSendEventType.Message, GetLegalMoreInfo(moreInfo));

            Emit(sendEvent);
        }

        public void SendCode(string phoneNumber, string code, List<string> serviceNames, SmsSendEventMoreInfo moreInfo = null) {
            SendCode(phoneNumber, code, GetLegalMoreInfo(moreInfo, serviceNames));
        }

        public void SendCode(List<string> phoneNumbers, string code, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumbers, SmsSendEventType.Message, GetLegalMoreInfo(moreInfo));

            Emit(sendEvent);
        }

        public void SendCode(List<string> phoneNumbers, string code, List<string> serviceNames, SmsSendEventMoreInfo moreInfo = null) {
            SendCode(phoneNumbers, code, GetLegalMoreInfo(moreInfo, serviceNames));
        }

        public void SendTemplateMessage(string phoneNumber, string templateCode, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumber, SmsSendEventType.Message, GetLegalMoreInfo(moreInfo));

            Emit(sendEvent);
        }

        public void SendTemplateMessage(string phoneNumber, string templateCode, List<string> serviceNames, SmsSendEventMoreInfo moreInfo = null) {
            SendTemplateMessage(phoneNumber, templateCode, GetLegalMoreInfo(moreInfo, serviceNames));
        }

        public void SendTemplateMessage(List<string> phoneNumbers, string templateCode, SmsSendEventMoreInfo moreInfo = null) {
            var sendEvent = new SmsSendEvent(phoneNumbers, SmsSendEventType.Message, GetLegalMoreInfo(moreInfo));

            Emit(sendEvent);
        }

        public void SendTemplateMessage(List<string> phoneNumbers, string templateCode, List<string> serviceNames, SmsSendEventMoreInfo moreInfo = null) {
            SendTemplateMessage(phoneNumbers, templateCode, GetLegalMoreInfo(moreInfo, serviceNames));
        }

        private void Emit(SmsSendEvent sendEvent) {
            Task.Factory.StartNew(async () => await _smsAggregationSender.SendAsync(sendEvent));
        }

        private SmsSendEventMoreInfo GetLegalMoreInfo(SmsSendEventMoreInfo moreInfo = null, List<string> serviceNames = null) {
            var info = moreInfo ?? new SmsSendEventMoreInfo();

            if (serviceNames != null && serviceNames.Any()) {
                if (info.SmsServiceNames == null || !info.SmsServiceNames.Any()) {
                    info.SmsServiceNames = serviceNames;
                } else {
                    var listInInfo = info.SmsServiceNames;
                    listInInfo.AddRange(serviceNames);
                    info.SmsServiceNames = listInInfo.Distinct().ToList();
                }
            }

            info.LowLevelSmsServiceNames = _specificImplementList;
            return info;
        }

        public void Dispose() { }
    }
}