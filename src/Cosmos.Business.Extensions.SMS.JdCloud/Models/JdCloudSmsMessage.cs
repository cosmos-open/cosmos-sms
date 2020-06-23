using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.JdCloud.Core;

namespace Cosmos.Business.Extensions.SMS.JdCloud.Models
{
    public class JdCloudSmsMessage
    {
        public List<string> PhoneNumberList { get; set; } = new List<string>();

        public List<string> Params { get; set; } = new List<string>();

        public void CheckParameters()
        {
            if (!PhoneNumberList.Any())
            {
                throw new InvalidArgumentException("收件人为空", JdCloudSmsConstants.ServiceName, 401);
            }

            if (PhoneNumberList.Count > 100)
            {
                throw new InvalidArgumentException("收件人不能超过 100 个", JdCloudSmsConstants.ServiceName, 402);
            }

            if (!Params.Any())
            {
                throw new InvalidArgumentException("内容不能为空", JdCloudSmsConstants.ServiceName, 401);
            }
        }
    }
}