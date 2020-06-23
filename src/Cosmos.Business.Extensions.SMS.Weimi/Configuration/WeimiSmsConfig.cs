namespace Cosmos.Business.Extensions.SMS.Weimi.Configuration
{
    public class WeimiSmsConfig : IConfig<WeimiSmsAccount>
    {
        public WeimiSmsAccount Account { get; set; }
        public bool Security { get; set; }
        public int RetryTimes { get; set; } = 3;
    }
}