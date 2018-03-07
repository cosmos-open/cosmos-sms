using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Business.Extensions.SMS.Abstractions.Exceptions;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Exceptions
{
    public class ChuangLanException : Exception, ISMSException
    {
        public long SerialVersionUID { get; protected set; }

        public int ErrorCode { get; set; }

        public ChuangLanException(string message, long serialVersionUid, int errorCode) : base(message)
        {
            SerialVersionUID = serialVersionUid;
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"code:{ErrorCode},message:{Message}";
        }
    }
}
