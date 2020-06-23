namespace Cosmos.Business.Extensions.SMS.Huyi.Configuration
{
    public class HuyiConfig : IConfig<HuyiAccount>
    {
        public HuyiAccount Account { get; set; }

        public int RetryTimes { get; set; } = 3;

        public int Timeout { get; set; } = 60000;
    }
}