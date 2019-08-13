using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Luosimao.Core;

namespace Cosmos.Business.Extensions.SMS.Luosimao.Models
{
    public class LuosimaoSmsMessage
    {
        public string PhoneNumber { get; set; }

        public string Content { get; set; }

        public void CheckParameters()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                throw new InvalidArgumentException("收信人为空", LuosimaoConstants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new InvalidArgumentException("验证码为空", LuosimaoConstants.ServiceName, 401);
            }
        }
    }
}