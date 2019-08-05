using System.Collections.Generic;
using System.Net.Http;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Configuration;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Core;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Core.Helpers;

namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Models
{
    public abstract class KsyunSms
    {
        public string Mobile { get; set; }

        public string SignName { get; set; }

        public string TplId { get; set; }

        public KeyValuePair<string, string> TplParams { get; set; }

        public string Content { get; set; }

        public abstract int SmsType { get; }

        public virtual void CheckParameters()
        {
            if (string.IsNullOrWhiteSpace(Mobile))
            {
                throw new InvalidArgumentException("收信人为空", KsyunSmsConstants.ServiceName, 401);
            }

            if (string.IsNullOrWhiteSpace(TplId))
            {
                throw new InvalidArgumentException("模板ID", KsyunSmsConstants.ServiceName, 401);
            }
        }

        public Dictionary<string, string> ToRequestObject()
        {
            return new Dictionary<string, string>
            {
                {"Mobile", Mobile},
                {"TplId", TplId},
                {"TplParams", JsonHelper.ToJson(TplParams)},
                {"SignName", SignName},
                {"Action", "SendSms"},
                {"Version", "2019-05-01"},
                {"SmsType", $"{SmsType}"}
            };
        }

        public AwsV4.SignedResult GetSignedResult(KsyunConfig config, FormUrlEncodedContent content)
        {
            var headers = new Dictionary<string, string> {{"host", KsyunSmsConstants.Host}};
            var awsv4 = new AwsV4(config, "POST", KsyunSmsConstants.Host, headers, string.Empty, content.ToString());
            return awsv4.Signer();
        }
    }
}