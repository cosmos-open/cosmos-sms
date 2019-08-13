using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Huyi.Core;

namespace Cosmos.Business.Extensions.SMS.Huyi.Models
{
    public class HuyiMessage
    {
        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public void CheckParameters()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                throw new InvalidArgumentException("收信人为空", HuyiConstants.ServiceName, 401);
            }


            if (string.IsNullOrWhiteSpace(Message))
            {
                throw new InvalidArgumentException("信息不能为空", HuyiConstants.ServiceName, 401);
            }
        }
    }
}