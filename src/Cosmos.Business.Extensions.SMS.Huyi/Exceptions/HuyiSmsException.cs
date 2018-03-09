namespace Cosmos.Business.Extensions.SMS.Huyi.Exceptions {
    public class HuyiSmsException : HuyiException {
        public HuyiSmsException(string message) : base(message, 1L, 401) { }
    }
}