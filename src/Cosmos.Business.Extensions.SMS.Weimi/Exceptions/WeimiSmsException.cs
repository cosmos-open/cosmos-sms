namespace Cosmos.Business.Extensions.SMS.Weimi.Exceptions {
    public class WeimiSmsException : WeimiException {
        public WeimiSmsException(string message) : base(message, 1L, 401) { }
    }
}