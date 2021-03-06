﻿namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Configuration
{
    public class YuntongxunSmsConfig : IConfig<YuntongxunAccount>
    {
        public YuntongxunAccount Account { get; set; }
        public bool Production { get; set; } = true;
        public int RetryTimes { get; set; } = 3;
    }
}