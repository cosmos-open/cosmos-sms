using Cosmos.Business.Extensions.SMS.Luosimao.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Luosimao.Models {
    public class LuosimaoSmsMessage {
        public string PhoneNumber { get; set; }
        public string Content { get; set; }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(this.PhoneNumber)) {
                throw new LuosimaoSmsException("收信人为空");
            }

            if (string.IsNullOrWhiteSpace(this.Content)) {
                throw new LuosimaoSmsException("验证码为空");
            }
        }
    }
}