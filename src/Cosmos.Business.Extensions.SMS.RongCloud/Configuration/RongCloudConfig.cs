namespace Cosmos.Business.Extensions.SMS.RongCloud.Configuration {
    public class RongCloudConfig : IConfig {
        public RongCloudAccount Account { get; set; }
        public int RetryTimes { get; set; }
    }
}