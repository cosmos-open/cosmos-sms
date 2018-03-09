namespace Cosmos.Business.Extensions.SMS.TencentCloud.Exceptions {
    public class TencentSmsException : TencentException {
        public TencentSmsException(string message) : base(message, 1L, 401) { }
    }
}