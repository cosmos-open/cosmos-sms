using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core.Helpers {
    /// <summary>
    /// Signature helper
    /// documentation:
    ///     https://help.aliyun.com/document_detail/30079.html?spm=5176.7739992.2.3.HM7WTG
    /// reference to:
    ///     https://github.com/yaosansi/aliyun-openapi-sdk-lite/blob/master/SignatureHelper.cs
    /// </summary>
    public static class SignatureHelper {
        public static string GetApiSignature(IDictionary<string, string> coll, string key) {
            var orgin = "POST&%2F&" +
                        PercentEncode(string.Join("&", coll.OrderBy(x => x.Key, StringComparer.Ordinal).Select(x => $"{PercentEncode(x.Key)}={PercentEncode(x.Value)}")));
            var sign = HMACSHA1HashingProvider.Signature(orgin, key + "&", Encoding.UTF8);
            return PercentEncode(sign);
        }

        private static string PercentEncode(string value) {
            var stringBuilder = new StringBuilder();
            var text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            var bytes = Encoding.UTF8.GetBytes(value);
            foreach (char c in bytes) {
                if (text.IndexOf(c) >= 0) {
                    stringBuilder.Append(c);
                } else {
                    stringBuilder.Append("%").Append(
                        string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int) c));
                }
            }

            return stringBuilder.ToString();
        }
    }
}