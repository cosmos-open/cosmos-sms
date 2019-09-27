namespace Cosmos.Business.Extensions.SMS.BaiduYun.Configuration
{
    public class BaiduYunConfig : IConfig<BaiduYunAccount>
    {
        public BaiduYunAccount Account { get; set; }

        public int TimeOut { get; set; } = 60000;

        /// <summary>
        /// 签名调用ID
        /// </summary>
        public string InvokeId { get; set; }

        public string Region { get; set; } = "Beijing";

        public bool Security { get; set; }

        public int RetryTimes { get; set; } = 3;
    }
}