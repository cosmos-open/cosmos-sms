namespace Cosmos.Business.Extensions.SMS.JdCloud.Configuration
{
    public class JdCloudSmsConfig : IConfig<JdCloudAccount>
    {
        public JdCloudAccount Account { get; set; }

        public string RegionId { get; set; }

        public string TemplateId { get; set; }

        public string SignId { get; set; }

        public bool Security { get; set; }

        public int RetryTimes { get; set; } = 3;

        public int RequestTimeout { get; set; } = 5;
    }
}