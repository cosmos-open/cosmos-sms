namespace Cosmos.Business.Extensions.SMS.ChuangLan.Configuration
{
    public class ChuanglanConfig : IConfig<ChuanglanAccount>
    {
        public ChuanglanAccount Account { get; set; }

        public int RetryTimes { get; set; } = 3;

        public int Timeout { get; set; } = 60000;
    }
}