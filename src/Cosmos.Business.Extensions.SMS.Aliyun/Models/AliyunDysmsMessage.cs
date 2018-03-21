using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.Aliyun.Configuration;
using Cosmos.Business.Extensions.SMS.Aliyun.Core;
using Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions;
using Cosmos.Business.Extensions.SMS.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Models {
    public class AliyunDysmsMessage {
        /// <summary>
        /// 短信模板Code，应严格按"模板CODE"填写, 请参考: https://dysms.console.aliyun.com/dysms.htm#/develop/template，必填
        /// </summary>
        public string TemplateCode { get; set; }

        public List<string> Phone { get; set; } = new List<string>();

        public Dictionary<string, string> TemplateParams { get; set; } = new Dictionary<string, string>();

        public string OutId { get; set; }

        public string GetPhoneString() => string.Join(",", Phone);

        public string GetTemplateParamsString() => TemplateParams.ToJson();

        public bool HasTemplateParams() => TemplateParams.Any();

        public void FixParameters(AliyunDysmsConfig config) {
            if (string.IsNullOrWhiteSpace(TemplateCode))
                TemplateCode = config.TemplateCode;
        }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(TemplateCode)) {
                throw new InvalidArgumentException("短信模板 Code 不能为空", Constants.ServiceName, 401);
            }

            var phoneCount = Phone?.Count;
            if (phoneCount == 0) {
                throw new InvalidArgumentException("收信人为空", Constants.ServiceName, 401);
            }

            if (phoneCount > Constants.MaxReceivers) {
                throw new InvalidArgumentException("收信人超过限制", Constants.ServiceName, 401);
            }
        }
    }
}