using System;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Configuration {
    public class ChuangLanAccount : IAccountSettings {
        public string User { get; set; }
        public string Key { get; set; }

        public string SmsUser => User;
        public string SmsKey => Key;

        public string ApiUrl { get; set; }

        public string Signature { get; set; }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(SmsUser)) {
                throw new ArgumentNullException(nameof(SmsUser));
            }

            if (string.IsNullOrWhiteSpace(SmsKey)) {
                throw new ArgumentNullException(nameof(SmsKey));
            }
        }
    }
}