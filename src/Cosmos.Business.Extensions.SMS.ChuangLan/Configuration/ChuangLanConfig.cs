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

        public string CodeTemplate { get; set; }

        public bool UseMarketingSms { get; set; } = false;

        public int RetryTimes { get; set; } = 3;
    }
}
