using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions;
using Cosmos.Business.Extensions.SMS.Aliyun.Exceptions;

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

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(TemplateCode)) {
                throw new AliyunDysmsException("短信模板 Code 不能为空");
            }

            var phoneCount = Phone?.Count;
            if (phoneCount == 0) {
                throw new AliyunDysmsException("收信人为空");
            }
        }
    }
}