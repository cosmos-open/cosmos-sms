using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Business.Extensions.SMS.Exceptions {
    public class SmsAggregationException : ISmsException {
        private readonly List<Exception> _exceptions;

        private static string NoneExceptionString = "There are no exception in this sms aggregation exception.";
        private static string HaveExceptionString = "There are several exceptions tin this sms aggregation exception.";

        public SmsAggregationException() {
            _exceptions = new List<Exception>();
        }

        public string Message => HasExceptions() ? HaveExceptionString : NoneExceptionString;

        public IReadOnlyList<Exception> Exceptions;

        public bool HasExceptions() => _exceptions.Any();
    }
}