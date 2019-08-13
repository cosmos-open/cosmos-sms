using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Yunpian.Core;

namespace Cosmos.Business.Extensions.SMS.Yunpian.Models
{
    public class YunpianSmsCode
    {
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public string Extend { get; set; }
        public string Uid { get; set; }
        public bool? Register { get; set; }

        public void CheckParameters()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                throw new InvalidArgumentException("收件人为空", Constants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new InvalidArgumentException("信息为空", Constants.ServiceName, 401);
            }
        }
    }
}