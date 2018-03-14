namespace Cosmos.Business.Extensions.SMS.RongCloud.Models {
    public class RongCloudSignatureBag {
        public string AppKey { get; set; }
        public string Nonce { get; set; }
        public string Timestamp { get; set; }
        public string Signature { get; set; }
    }
}