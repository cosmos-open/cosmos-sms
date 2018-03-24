namespace Cosmos.Business.Extensions.SMS.Huyi.Configuration {
    public class HuyiConfig : IConfig {
        public HuyiAccount Account { get; set; }
        public int RetryTimes { get; set; } = 3;
    }
}