using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Core.Helpers
{
    /// <summary>
    /// SMS Signature helper
    /// documentation: http://sendcloud.sohu.com/doc/sms/#api
    /// </summary>
    public static class SignatureHelper
    {
        public static string GetApiSignature(IDictionary<string, string> coll, string key)
            => MD5HashingProvider.Signature($"{key}&{string.Join("&", coll.Select(x => $"{x.Key}={x.Value}"))}&{key}");

        /// <summary>
        /// To varify signature
        /// documentation: http://sendcloud.sohu.com/doc/sms/#smshook
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static bool HookVerify(string key, string token, long timestamp, string signature)
            => HMACSHA256HashingProvider.Verify(signature, $"{timestamp}{token}", key, Encoding.UTF8);
    }
}