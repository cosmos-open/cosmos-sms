using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Cosmos.Business.Extensions.SMS.BaiduYun.Core;
using System.Text;
using Cosmos.Business.Extensions.SMS.BaiduYun.Configuration;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Models {
    public class BceObject {
        private readonly BaiduYunConfig _config;

        public BceObject(BaiduYunConfig config) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private string accessKeyId => _config.Account.AccessKeyId;

        private string secretAccessKey => _config.Account.SecretAccessKey;

        private int expireationPeriodInSeconds => _config.Account.ExpirationPeriodInSeconds;

        /// <summary>
        /// 获取验证字符串
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <param name="canonicalRequest">非匿名请求中必须携带的认证信息。包含生成待签名串CanonicalRequest所必须的信息以及签名摘要signature。</param>
        /// <param name="signedHeaders">所有在这一阶段进行了编码的Header名字转换成全小写之后按照字典序排列，然后用分号（;）连接</param>
        /// <returns></returns>
        public string GetAuthString(DateTime date, string canonicalRequest, string signedHeaders) {
            var timestamp = GetTimeStamp(date);
            var prefix = $"bce-auth-v1/{accessKeyId}/{timestamp}/{expireationPeriodInSeconds}";
            var signingkey = GetSigningKeyByHMACSHA256HEX(secretAccessKey, prefix);
            var signature = GetSignatureByHMACSHA256HEX(signingkey, canonicalRequest);
            return $"{prefix}/{signedHeaders}/{signature}";
        }

        /// <summary>
        /// 获取验证字符串
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <param name="canonicalRequest">非匿名请求中必须携带的认证信息。包含生成待签名串CanonicalRequest所必须的信息以及签名摘要signature。</param>
        /// <param name="signedHeaders">所有在这一阶段进行了编码的Header名字</param>
        /// <returns></returns>
        public string GetAuthString(DateTime date, string canonicalRequest, IEnumerable<string> signedHeaders) {
            return GetAuthString(date, canonicalRequest, signedHeaders.Select(s => s.ToLower()).OrderBy(s => s).Merge(";"));
        }

        /// <summary>
        /// 获取日期时间的时间戳格式
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <returns></returns>
        private static string GetTimeStamp(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm:ssZ");

        /// <summary>
        /// 获取 signingKey。百度云不直接使用SK对待签名串生成摘要。相反的，百度云首先使用SK和认证字符串前缀生成signingKey，然后用signingKey对待签名串生成摘要。
        /// </summary>
        /// <param name="secretAccessKey">用户SK</param>
        /// <param name="authStringPrefix">认证字符串的前缀部分</param>
        /// <returns></returns>
        private static string GetSigningKeyByHMACSHA256HEX(string secretAccessKey, string authStringPrefix) {
            return HMACSHA256HashingProvider.Signature(authStringPrefix, secretAccessKey, Encoding.UTF8).ToLower();
//            HMACSHA256 Livehmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretAccessKey));
//            byte[] LiveHash = Livehmacsha256.ComputeHash(Encoding.UTF8.GetBytes(authStringPrefix));
//            string SigningKey = HashEncode(LiveHash);
//            return SigningKey;
        }

        /// <summary>
        /// 获取 签名摘要。百度云使用signingKey对canonicalRequest使用HAMC算法计算签名。
        /// </summary>
        /// <param name="signingKey">签名Key。百度云不直接使用SK对待签名串生成摘要。相反的，百度云首先使用SK和认证字符串前缀生成signingKey，然后用signingKey对待签名串生成摘要。</param>
        /// <param name="canonicalRequest">非匿名请求中必须携带的认证信息。包含生成待签名串CanonicalRequest所必须的信息以及签名摘要signature。</param>
        /// <returns></returns>
        private static string GetSignatureByHMACSHA256HEX(string signingKey, string canonicalRequest) {
            return HMACSHA256HashingProvider.Signature(canonicalRequest, signingKey, Encoding.UTF8).ToLower();
//            HMACSHA256 Livehmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(signingKey));
//            byte[] LiveHash = Livehmacsha256.ComputeHash(Encoding.UTF8.GetBytes(canonicalRequest));
//            string Signature = HashEncode(LiveHash);
//            return Signature;
        }


//        /// <summary>
//        /// 将摘要字符串规范化
//        /// </summary>
//        /// <param name="hash">已有摘要</param>
//        /// <returns></returns>
//        private static string HashEncode(byte[] hash) {
//            return BitConverter.ToString(hash).Replace("-", "").ToLower();
//        }
    }
}