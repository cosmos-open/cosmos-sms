#region License

/*
 * All content copyright yaosansi, SeayXu, unless otherwise indicated. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 *
 */

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core.Helpers
{
    /// <summary>
    /// Signature helper
    /// documentation:
    ///     https://help.aliyun.com/document_detail/30079.html?spm=5176.7739992.2.3.HM7WTG
    /// reference to:
    ///     https://github.com/yaosansi/aliyun-openapi-sdk-lite/blob/master/SignatureHelper.cs
    /// </summary>
    public static class SignatureHelper
    {
        // ReSharper disable once InconsistentNaming
        private const string PERCENT_ENCODE_TEXT = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public static string GetApiSignature(IDictionary<string, string> coll, string key)
        {
            var origin = coll.GetOrigin();

            var sign = origin.Signature(key);

            // hex string to byte array
            var buffer = sign.HexToBytes();

            // convert bytes to base64 string and return
            return Convert.ToBase64String(buffer);
        }

        private static string GetOrigin(this IDictionary<string, string> coll)
        {
            return $"POST&%2F&{PercentEncode(string.Join("&", coll.OrderBy(x => x.Key, StringComparer.Ordinal).Select(x => $"{PercentEncode(x.Key)}={PercentEncode(x.Value)}")))}";
        }

        private static string Signature(this string orgin, string key)
        {
            return HMACSHA1HashingProvider.Signature(orgin, $"{key}&", Encoding.UTF8);
        }

        private static string PercentEncode(string value)
        {
            var stringBuilder = new StringBuilder();

            var bytes = Encoding.UTF8.GetBytes(value);

            foreach (var b in bytes)
            {
                var c = (char)b;

                if (PERCENT_ENCODE_TEXT.IndexOf(c) >= 0)
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder
                        .Append("%")
                        .Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)c));
                }
            }

            return stringBuilder.ToString();
        }
    }
}