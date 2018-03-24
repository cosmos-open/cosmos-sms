namespace Cosmos.Business.Extensions.SMS.BaiduYun.Configuration {
    public class BaiduYunAccount : IAccountSettings {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }

        /// <summary>
        /// 认证访问超时，单位：秒
        /// </summary>
        public int ExpirationPeriodInSeconds { get; set; } = 1800;
    }
}