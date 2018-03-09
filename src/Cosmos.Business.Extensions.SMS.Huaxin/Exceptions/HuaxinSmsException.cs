namespace Cosmos.Business.Extensions.SMS.Huaxin.Exceptions {
    public class HuaxinSmsException : HuaxinException {
        public HuaxinSmsException(string message) : base(message, 1L, 401) { }
    }
}