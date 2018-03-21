using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Events {
    public class SmsSendEventMoreInfo {
        public string NationCode { get; set; } = "86";
        public List<string> SmsServiceNames { get; set; }
        internal List<string> LowLevelSmsServiceNames { get; set; }
        public Action<SmsAggregationException> ExceptionHandler { get; set; }
        public Action<SmsSendFeedback> Callback { get; set; }
    }
}