namespace Cosmos.Business.Extensions.SMS.SubMail.Exceptions {
    public class SubMailSmsException : SubMailException {
        public SubMailSmsException(string message) : base(message, 1L, 401) { }
    }
}