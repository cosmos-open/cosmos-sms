namespace Cosmos.Business.Extensions.SMS.Luosimao.Configuration
{
    public class LuosimaoConfig : IConfig
    {
        public LuosimaoAccount Account { get; set; }
        public string SignName { get; set; }
        public int RetryTimes { get; set; } = 3;
    }
}