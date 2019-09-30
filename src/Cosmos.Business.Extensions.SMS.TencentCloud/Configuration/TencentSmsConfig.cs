namespace Cosmos.Business.Extensions.SMS.TencentCloud.Configuration
{
    public class TencentSmsConfig : IConfig<TencentAccount>
    {
        public TencentAccount Account { get; set; }
    }
}