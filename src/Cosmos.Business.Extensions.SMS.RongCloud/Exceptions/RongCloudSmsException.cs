namespace Cosmos.Business.Extensions.SMS.RongCloud.Exceptions {
    public class RongCloudSmsException : RongCloudException {
        public RongCloudSmsException(string message) : base(message, 1L, 401) { }
    }
}