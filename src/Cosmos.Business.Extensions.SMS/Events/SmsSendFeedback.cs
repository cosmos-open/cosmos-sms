using System;
using Cosmos.Business.Extensions.SMS.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Events
{
    public class SmsSendFeedback
    {
        private readonly SmsAggregationException _exception;

        public SmsSendFeedback(string name, int successCount, ISmsAggregationExceptionFactory exceptionFactory)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            ServiceName = name;
            SuccessTaskCount = successCount;
            _exception = exceptionFactory?.Create() ?? throw new ArgumentNullException(nameof(exceptionFactory));
        }

        public string ServiceName { get; }

        public int SuccessTaskCount { get; }

        public SmsAggregationException Exception => _exception;
    }
}