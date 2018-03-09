using Cosmos.Business.Extensions.SMS.Yunpian.Exceptions;

namespace Cosmos.Business.Extensions.SMS.Yunpian.Models {
    public class YunpianSmsCode {
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public string Extend { get; set; }
        public string Uid { get; set; }
        public bool? Register { get; set; }

        public void CheckParameters() {
            if (string.IsNullOrWhiteSpace(PhoneNumber)) {
                throw new YunpianSmsException("收件人为空");
            }

            if (string.IsNullOrWhiteSpace(Content)) {
                throw new YunpianSmsException("信息为空");
            }
        }
    }
}