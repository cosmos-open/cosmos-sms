namespace Cosmos.Business.Extensions.SMS.Yunpian.Configuration
{
    public class YunpianConfig : IConfig<YunpianAccount>
    {
        public YunpianAccount Account { get; set; }
        public string CallbackUrl { get; set; }
    }
}