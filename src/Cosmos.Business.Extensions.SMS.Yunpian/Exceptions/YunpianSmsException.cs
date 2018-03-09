namespace Cosmos.Business.Extensions.SMS.Yunpian.Exceptions {
    public class YunpianSmsException : YunpianException {
        public YunpianSmsException(string message) : base(message, 1L, 401) { }
    }
}