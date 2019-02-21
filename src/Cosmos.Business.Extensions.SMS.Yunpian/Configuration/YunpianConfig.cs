namespace Cosmos.Business.Extensions.SMS.Yunpian.Configuration
{
    public class YunpianConfig : IConfig
    {
        public YunpianAccount Account { get; set; }
        public string CallbackUrl { get; set; }
    }
}