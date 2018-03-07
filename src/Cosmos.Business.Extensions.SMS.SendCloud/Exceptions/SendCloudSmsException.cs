namespace Cosmos.Business.Extensions.SMS.SendCloud.Exceptions {
    public class SendCloudSmsException : SendCloudException {
        public SendCloudSmsException(string message) : base(message, 1L, 401) { }
    }
}