using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Core;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Models
{
    public class HuaweiCloudSmsMessage
    {
        public List<PhoneNumberEntity> PhoneNumberList { get; set; } = new List<PhoneNumberEntity>();

        public List<string> Params { get; set; } = new List<string>();

        public void CheckParameters()
        {
            if (!PhoneNumberList.Any())
            {
                throw new InvalidArgumentException("收件人为空", HuaweiCloudSmsConstants.ServiceName, 401);
            }

            if (PhoneNumberList.Count > 1000)
            {
                throw new InvalidArgumentException("收件人不能超过 1000 个", HuaweiCloudSmsConstants.ServiceName, 402);
            }

            if (PhoneNumberList.Any(p => !p.IsValid()))
            {
                throw new InvalidArgumentException("收件人号码长度不能超过 21 位", HuaweiCloudSmsConstants.ServiceName, 403);
            }
        }
    }
}