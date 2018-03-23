using System;
using System.Text;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Core.Helpers {
    public static class SignatureHelper {
        public static (string sig, string auth) GetSignature(string accountSid, string accountToken) {
            var date = DateTime.Now.ToString("yyyyMMddhhmmss");
            var sigstr = MD5HashingProvider.Signature($"{accountSid}{accountToken}{date}").ToUpper();
            var authStr = Base64ConvertProvider.Encode($"{accountSid}:{date}", Encoding.UTF8);
            return (sigstr, authStr);
        }
    }
}