using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Business.Extensions.SMS.Abstractions;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Configuration
{
    public class ChuangLanConfig:IConfig
    {
        public ChuangLanAccount CodeAccount { get; set; }

        public ChuangLanAccount MarketingAccount { get; set; }

        public int RetryTimes { get; set; } = 3;
    }
}
