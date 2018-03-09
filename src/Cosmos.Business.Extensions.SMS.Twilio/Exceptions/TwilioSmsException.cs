namespace Cosmos.Business.Extensions.SMS.Twilio.Exceptions {
    public class TwilioSmsException : TwilioException {
        public TwilioSmsException(string message) : base(message, 1L, 401) { }
    }
}