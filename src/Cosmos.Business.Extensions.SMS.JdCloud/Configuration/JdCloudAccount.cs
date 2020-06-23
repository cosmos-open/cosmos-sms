namespace Cosmos.Business.Extensions.SMS.JdCloud.Configuration
{
    public class JdCloudAccount : IAccountSettings
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }
    }
}