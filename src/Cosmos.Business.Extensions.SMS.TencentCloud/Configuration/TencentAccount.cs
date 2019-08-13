namespace Cosmos.Business.Extensions.SMS.TencentCloud.Configuration
{
    public class TencentAccount : IAccountSettings
    {
        public int AppId { get; set; }
        public string AppKey { get; set; }
    }
}