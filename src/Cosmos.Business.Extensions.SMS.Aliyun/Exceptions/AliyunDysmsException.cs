namespace Cosmos.Business.Extensions.SMS.Aliyun.Exceptions {
    public class AliyunDysmsException : AliyunException {
        public AliyunDysmsException(string message) : base(message, 1L, 401) { }
    }
}