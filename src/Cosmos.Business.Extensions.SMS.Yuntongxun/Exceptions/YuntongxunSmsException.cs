namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Exceptions {
    public class YuntongxunSmsException : YuntongxunException {
        public YuntongxunSmsException(string message) : base(message, 1L, 401) { }
    }
}