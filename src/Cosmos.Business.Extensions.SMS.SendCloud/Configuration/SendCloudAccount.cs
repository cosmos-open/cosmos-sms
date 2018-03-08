namespace Cosmos.Business.Extensions.SMS.SendCloud.Configuration {
    public class SendCloudAccount : IAccountSettings {
        public string User { get; set; }
        public string Key { get; set; }

        public string SmsUser => User;
        public string SmsKey => Key;
    }
}