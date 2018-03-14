using System;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Core {
    public static class SignatureHalper {
        public static (string nonce, string timestamp, string signature) GenerateSignature(string appSecret) {
            var rd = new Random((int) DateTime.Now.Ticks);
            var rd_i = rd.Next();
            var nonce = Convert.ToString(rd_i);
            var timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.UtcNow));
            var signature = SHA1HashingProvider.Signature(appSecret + nonce + timestamp);
            return (nonce, timestamp, signature);
        }

        private static int ConvertDateTimeInt(DateTime time) {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int) (time - startTime).TotalSeconds;
        }
    }
}