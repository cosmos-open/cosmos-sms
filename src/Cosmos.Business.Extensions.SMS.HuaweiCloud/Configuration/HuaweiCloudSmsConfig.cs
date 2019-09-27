namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Configuration
{
    public class HuaweiCloudSmsConfig : IConfig<HuaweiCloudAccount>
    {
        public HuaweiCloudAccount Account { get; set; }

        public string RegionName { get; set; } = "cn-north-1";

        public string Sender { get; set; }

        public string TemplateId { get; set; }

        public string Signature { get; set; }

        public int RetryTimes { get; set; } = 3;

        public int TimeOut { get; set; } = 60000;

        public string StatusCallBackUri { get; set; }
    }
}