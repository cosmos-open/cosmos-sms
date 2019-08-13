using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Configuration;

namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Core
{
    /// <summary>
    /// Aws v4
    /// <br />
    /// See more: https://docs.ksyun.com/documents/6482?preview=1
    /// </summary>
    public class AwsV4
    {
        // ReSharper disable once InconsistentNaming
        private readonly string __ak;

        // ReSharper disable once InconsistentNaming
        private readonly string __sk;

        // ReSharper disable once InconsistentNaming
        private readonly string __region;

        // ReSharper disable once InconsistentNaming
        private readonly string __service;

        // ReSharper disable once InconsistentNaming
        private string __method;

        // ReSharper disable once InconsistentNaming
        private string __uri;

        // ReSharper disable once InconsistentNaming
        private Dictionary<string, string> __headers;

        // ReSharper disable once InconsistentNaming
        private string __query;

        // ReSharper disable once InconsistentNaming
        private string __body;

        // ReSharper disable once InconsistentNaming
        public readonly SHA256 ___sha256;

        public AwsV4(KsyunConfig config, string method, string uri, Dictionary<string, string> headers, string query, string body)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (config.Account == null)
                throw new ArgumentNullException(nameof(config.Account));

            __ak = config.Account.AccessKeyId;
            __sk = config.Account.AccessKeySecret;
            __region = config.Region;
            __service = config.Service;
            __method = method;
            __uri = uri;
            __headers = headers;
            __query = query;
            __body = body;
            ___sha256 = SHA256.Create();
        }

        private static string ToHexString(byte[] array)
        {
            var hex = new StringBuilder(array.Length * 2);
            foreach (var b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        private static byte[] Sign(byte[] key, string data)
            => new HMACSHA256(key).ComputeHash(Encoding.UTF8.GetBytes(data));

        // ReSharper disable once UnusedMember.Local
        private string HexEncodeHash(byte[] bytesToHash)
            => ToHexString(___sha256.ComputeHash(bytesToHash));

        private string HexEncodeHash(string text)
            => ToHexString(___sha256.ComputeHash(Encoding.UTF8.GetBytes(text)));

        private static string CreateCanonicalUri(string uri)
            => uri.StartsWith("/") ? uri : string.Concat("/", uri);

        public string ToStartHex(string data, bool withSeparationChar)
        {
            var bytes = Encoding.GetEncoding("utf-8").GetBytes(data);
            var str = "";
            for (var i = 0; i < bytes.Length; i++)
            {
                str += $"{bytes[i]:X}";
                if (withSeparationChar && (i != bytes.Length - 1))
                {
                    str += ",";
                }
            }

            return str.ToLower();
        }

        public string GetCanonicalQueryParams(string query)
        {
            var querystring = HttpUtility.ParseQueryString(query);
            var keys = querystring.AllKeys;
            Array.Sort(keys, (a, b) => string.CompareOrdinal(ToStartHex(a, false), ToStartHex(b, false)));
            // Query params must be escaped in upper case (i.e. "%2C", not "%2c").
            var queryParams = keys.Select(key => $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(querystring[key])}");
            var canonicalQueryParams = string.Join("&", queryParams);
            return canonicalQueryParams;
        }

        // ReSharper disable once InconsistentNaming
        private static string ComputeSignature(string sk, string subTime, string region, string service, string signString, string AWS4_REQUEST)
        {
            var kDate = Sign(Encoding.UTF8.GetBytes("AWS4" + sk), subTime);
            var kRegion = Sign(kDate, region);
            var kService = Sign(kRegion, service);
            var kSigning = Sign(kService, AWS4_REQUEST);
            return ToHexString(Sign(kSigning, signString));
        }

        private static string CanonicalHeader(Dictionary<string, string> headers, string[] signedHeaders)
            => string.Join("\n", signedHeaders.Select(header => header + ":" + headers[header]).ToArray()) + "\n";

        private static Dictionary<string, string> TransHeaders(Dictionary<string, string> headers)
            => headers.Keys.ToDictionary(header => header.ToLowerInvariant().Trim(), header => headers[header].Trim());

        public class SignedResult
        {
            public string Authorization;
            public string Time;
        }

        public SignedResult Signer()
        {
            var AWS4_REQUEST = "aws4_request";
            var Algorithm = "AWS4-HMAC-SHA256";

            var t = DateTimeOffset.UtcNow;

            var time = t.ToString("yyyyMMddTHHmmssZ");
            // Time = "20190514T030223Z";

            var subTime = time.Substring(0, 8);

            var credentialScope = string.Join("/", subTime, __region, __service, AWS4_REQUEST);

            var allHeaders = TransHeaders(__headers);
            if (allHeaders.ContainsKey("x-amz-date"))
            {
                allHeaders.Remove("x-amz-date");
            }

            allHeaders.Add("x-amz-date", time);

            var signedHeaders = new string[allHeaders.Keys.Count];
            allHeaders.Keys.CopyTo(signedHeaders, 0);

            if (Array.IndexOf(signedHeaders, "host") < 0)
            {
                throw new ArgumentException("headers host must exist", $"original");
            }

            Array.Sort(signedHeaders, string.Compare);

            var canonicalSignedHeaders = string.Join(";", signedHeaders);

            var canonicalRequest = string.Join("\n",
                __method,
                CreateCanonicalUri(__uri),
                GetCanonicalQueryParams(__query),
                CanonicalHeader(allHeaders, signedHeaders),
                canonicalSignedHeaders,
                HexEncodeHash(__body));


            var signString = string.Join("\n",
                Algorithm,
                time,
                credentialScope,
                HexEncodeHash(canonicalRequest));

            var signature = ComputeSignature(
                __sk,
                subTime,
                __region,
                __service,
                signString,
                AWS4_REQUEST
            );

            var authorization = string.Join(", ",
                Algorithm + " Credential=" + __ak + "/" + credentialScope,
                "SignedHeaders=" + canonicalSignedHeaders,
                "Signature=" + signature);

            return new SignedResult {Authorization = authorization, Time = time};
        }
    }
}