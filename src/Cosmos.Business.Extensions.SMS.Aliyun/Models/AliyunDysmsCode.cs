using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions;
using Cosmos.Business.Extensions.SMS.Aliyun.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Models {
    public class AliyunDysmsCode : ISMSCode {
        /// <summary>
        /// 短信模板Code，应严格按"模板CODE"填写, 请参考: https://dysms.console.aliyun.com/dysms.htm#/develop/template，必填
        /// </summary>
        public string TemplateCode { get; set; }

        public List<string> Phone { get; set; } = new List<string>();

        public string Code { get; set; }

        public string OutId { get; set; }

        public string GetPhoneString() => string.Join(",", Phone);

        public string GetTemplateParamsString() => new {code = Code}.ToJson();

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(TemplateCode)) {
                throw new AliyunDysmsException("短信模板 Code 不能为空");
            }

            var phoneCount = Phone?.Count;
            if (phoneCount == 0) {
                throw new AliyunDysmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(Code)) {
                throw new AliyunDysmsException("验证码不能为空");
            }
            
            if (phoneCount > Core.Constants.MaxReceivers) {
                throw new AliyunDysmsException("收信人超过限制");
            }
        }
    }
}