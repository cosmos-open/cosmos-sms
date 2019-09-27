using Cosmos.Business.Extensions.SMS.HuaweiCloud.Configuration;
using WebApiClient.DataAnnotations;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core
{
    public class WsseObject
    {
        public WsseObject(HuaweiCloudAccount account)
        {
            AppKey = account.AppKey;
            AppSecret = account.AppSecret;
        }

        [IgnoreSerialized]
        public string AppKey { get; set; }

        [IgnoreSerialized]
        public string AppSecret { get; set; }
    }
}