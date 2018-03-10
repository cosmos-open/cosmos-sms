using System;
using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS.Events {
    public class SmsSendEventMoreInfo {
        public int NationCode { get; set; } = 86;
        public List<string> SmsServiceNames { get; set; }
        public Action<Exception> ExceptionHandler { get; set; }
    }
}