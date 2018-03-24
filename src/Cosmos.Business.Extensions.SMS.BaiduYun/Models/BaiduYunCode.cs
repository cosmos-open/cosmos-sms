using Cosmos.Business.Extensions.SMS.BaiduYun.Core;
using Cosmos.Business.Extensions.SMS.Exceptions;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Models {
    public class BaiduYunCode : BaiduYunMessage {
        public string Code { get; set; }
        public string CodeKey { get; set; } = "code";

        public override void CheckParameters() {

            if (string.IsNullOrWhiteSpace(Code)) {
                throw new InvalidArgumentException("验证码不能为空", Constants.ServiceName, 401);
            }

            base.CheckParameters();
        }
    }
}