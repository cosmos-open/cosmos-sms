using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.ChuangLan.Core;
using Cosmos.Business.Extensions.SMS.Exceptions;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models
{
    public class ChuanglanSmsVariableMessage : ChuanglanSmsBase
    {
        public string Content { get; set; }

        public List<string> Params { get; set; } = new List<string>();

        public string GetParamsString() => string.Join(";", Params);

        public void CheckParameters()
        {
            var phoneCount = Params?.Count;

            if (phoneCount == 0)
            {
                throw new InvalidArgumentException("收信人为空", ChuanglanConstants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new InvalidArgumentException("信息为空", ChuanglanConstants.ServiceName, 401);
            }
        }
    }
}