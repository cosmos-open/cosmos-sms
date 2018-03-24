using Cosmos.Business.Extensions.SMS.BaiduYun.Configuration;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Models {
    public class BceObjectWrapper {
        public BceObjectWrapper(BceObject bce, BaiduYunMessage message, BaiduYunConfig config) {
            BceObject = bce;
            Message = message;
            Config = config;
        }

        public BceObject BceObject { get; set; }
        public BaiduYunMessage Message { get; set; }
        public BaiduYunConfig Config { get; set; }
        public string UrlSegment { get; set; } = "bce/v2/message";
        public string ApiServerUrl { get; set; }
    }
}