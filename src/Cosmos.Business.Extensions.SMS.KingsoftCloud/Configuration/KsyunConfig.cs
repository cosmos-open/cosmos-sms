namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Configuration
{
    public class KsyunConfig : IConfig
    {
        public KsyunAccount Account { get; set; }

        public int TimeOut { get; set; } = 60000;

        public string Region { get; set; } = "cn-beijing-6";

        public string Service { get; set; }

        public string SignName { get; set; }

        public bool Security { get; set; }

        public int RetryTimes { get; set; } = 3;
    }
}