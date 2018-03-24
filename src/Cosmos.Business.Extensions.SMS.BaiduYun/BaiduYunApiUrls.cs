namespace Cosmos.Business.Extensions.SMS.BaiduYun {
    /// <summary>
    /// BaiduYun SMS service's url
    /// + 非BCC用户和“华北-北京”区域BCC用户可使用北京域名；
    /// + 如您使用“华南-广州”区域BCC服务请访问广州域名
    /// More infomation to see:
    ///     https://cloud.baidu.com/doc/SMS/API.html#SMS.E6.9C.8D.E5.8A.A1.E5.9F.9F.E5.90.8D
    /// </summary>
    public sealed class BaiduYunApiUrls {
        public const string BeijingService = "sms.bj.baidubce.com";
        public const string GuangzhouService = "sms.gz.baidubce.com";
    }
}