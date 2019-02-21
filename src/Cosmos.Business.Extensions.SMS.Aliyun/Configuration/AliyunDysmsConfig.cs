namespace Cosmos.Business.Extensions.SMS.Aliyun.Configuration
{
    public class AliyunDysmsConfig : IConfig
    {
        public AliyunDysmsAccount Account { get; set; }

        public string SignName { get; set; }

        public string TemplateCode { get; set; }

        public bool Security { get; set; }

        public int RetryTimes { get; set; } = 3;
    }
}