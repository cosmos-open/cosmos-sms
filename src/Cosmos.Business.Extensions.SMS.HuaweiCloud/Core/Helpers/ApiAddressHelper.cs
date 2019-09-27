namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Helpers
{
    internal static class ApiAddressHelper
    {
        public static string Get(string regionName)
        {
            return $"{GetGateway(regionName)}{GetRequestUri()}";
        }

        public static string GetGateway(string regionName)
        {
            return $"https://rtcsms.{regionName}.myhuaweicloud.com:10743";
        }

        public static string GetRequestUri()
        {
            return "/sms/batchSendSms/v1";
        }
    }
}