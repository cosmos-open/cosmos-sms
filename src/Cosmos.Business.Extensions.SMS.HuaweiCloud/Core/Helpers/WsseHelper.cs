using System;
using System.Text;
using Cosmos.Encryption;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Helpers
{
    internal static class WsseHelper
    {
        /// <summary>
        /// 构造 X-WSSE 参数值
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static string BuildWSSEHeader(string appKey, string appSecret)
        {
            var now = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); //Created
            var nonce = Guid.NewGuid().ToString().Replace("-", ""); //Nonce

            var material = Encoding.UTF8.GetBytes(nonce + now + appSecret);
            var hashed = SHA256HashingProvider.Signature(material);
            var hexDigest = BitConverter.ToString(hashed).Replace("-", "");
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(hexDigest)); //PasswordDigest

            return $"UsernameToken Username=\"{appKey}\",PasswordDigest=\"{base64}\",Nonce=\"{nonce}\",Created=\"{now}\"";
        }
    }
}