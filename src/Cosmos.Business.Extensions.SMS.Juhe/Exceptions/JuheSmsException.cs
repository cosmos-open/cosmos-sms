namespace Cosmos.Business.Extensions.SMS.Juhe.Exceptions {
    public class JuheSmsException : JuheException {
        public JuheSmsException(string message) : base(message, 1L, 401) { }
    }
}