using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Business.Extensions.SMS.ChuangLan.Exceptions;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models
{
    public class ChuangLanSmsVariableMessage:ChuangLanSmsBase
    {
        public string Content { get; set; }

        public List<string> Params { get; set; } = new List<string>();

        public string GetParamsString() => string.Join(";", Params);

        public void CheckParameters()
        {
            var phoneCount = Params?.Count;
            if (phoneCount == 0)
            {
                throw new ChuangLanSmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new ChuangLanSmsException("信息为空");
            }
        }
    }
}
