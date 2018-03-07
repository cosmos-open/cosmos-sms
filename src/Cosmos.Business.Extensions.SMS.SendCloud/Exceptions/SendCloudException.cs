using System;
using Cosmos.Business.Extensions.SMS.Abstractions.Exceptions;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Exceptions {
    public class SendCloudException : Exception, ISMSException {

        public long SerialVersionUID { get; protected set; }

        public int ErrorCode { get; protected set; }

        public SendCloudException(string message, long serialVersionUid, int errorCode) : base(message) {
            SerialVersionUID = serialVersionUid;
            ErrorCode = errorCode;
        }

        public override string ToString() => $"code:{ErrorCode},message:{Message}";
    }
}