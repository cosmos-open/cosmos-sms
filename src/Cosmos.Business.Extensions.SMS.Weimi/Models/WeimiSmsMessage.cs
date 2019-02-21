using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Weimi.Core;

namespace Cosmos.Business.Extensions.SMS.Weimi.Models
{
    public class WeimiSmsMessage
    {

        public string Content { get; set; }

        public List<string> PhoneNumbers { get; set; } = new List<string>();

        public string GetPhoneString() => string.Join(",", PhoneNumbers);

        public string Timing { get; set; }

        public void CheckParameters()
        {
            var phoneCount = PhoneNumbers?.Count;
            if (phoneCount == 0)
            {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (phoneCount > Core.Constants.MaxReceivers)
            {
                throw new InvalidArgumentException("收信人超过限制", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new InvalidArgumentException("内容不能为空", Constants.ServiceName, 401);
            }
        }
    }
}