using System;

namespace Cosmos.Business.Extensions.SMS.Exceptions {
    public class InvalidArgumentException : Exception, ISmsException {
        public InvalidArgumentException(string message, string serviceName, int errorCode) : base(message) {
            ErrorCode = errorCode;
            ServiceName = serviceName;
        }

        public string ServiceName { get; }

        public int ErrorCode { get; }

        public override string ToString() => $"code:{ErrorCode},message:{Message}";
    }
}