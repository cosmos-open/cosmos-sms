using System;

namespace Cosmos.Business.Extensions.SMS.Events {
    public class SmsSendFeedback {
        public SmsSendFeedback(string name, int successCount) {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            ServiceName = name;
            SuccessTaskCount = successCount;
        }

        public string ServiceName { get; }

        public int SuccessTaskCount { get; }
    }
}