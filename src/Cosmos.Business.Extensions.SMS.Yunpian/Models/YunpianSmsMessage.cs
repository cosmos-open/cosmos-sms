using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Yunpian.Core;

namespace Cosmos.Business.Extensions.SMS.Yunpian.Models
{
    public class YunpianSmsMessage
    {
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public string Content { get; set; }
        public string Extend { get; set; }

        public string GetPhoneString() => string.Join(",", PhoneNumbers);

        public void CheckParameters()
        {
            var phoneCount = PhoneNumbers?.Count;
            if (phoneCount == 0)
            {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new InvalidArgumentException("信息为空", Constants.ServiceName, 401);
            }
        }
    }
}