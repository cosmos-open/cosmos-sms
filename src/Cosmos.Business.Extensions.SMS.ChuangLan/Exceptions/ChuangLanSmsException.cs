using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Exceptions
{
    public class ChuangLanSmsException:ChuangLanException
    {
        public ChuangLanSmsException(string message) : base(message, 1L, 401)
        {
        }
    }
}
