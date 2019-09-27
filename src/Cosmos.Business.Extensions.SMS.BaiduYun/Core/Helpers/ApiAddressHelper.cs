using System;
using Cosmos.Business.Extensions.SMS.BaiduYun.Configuration;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core.Helpers
{
    internal static class ApiAddressHelper
    {
        public static string Get(BaiduYunConfig config)
        {
            return $"{GetHttpPrefix(config)}://{GetApiServerUrl(config)}";
        }

        public static string GetApiServerUrl(BaiduYunConfig config)
        {
            if (config == null || string.Compare(config.Region, "Beijing", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return BaiduYunApiUrls.BeijingService;
            }

            return BaiduYunApiUrls.GuangzhouService;
        }

        public static string GetHttpPrefix(BaiduYunConfig config)
        {
            if (config == null || !config.Security)
            {
                return "http";
            }

            return "https";
        }
    }
}