namespace Cosmos.Business.Extensions.SMS.Events.Restrict
{
    /// <summary>
    /// sms entry type
    /// </summary>
    public enum SmsSendEventType
    {
        /// <summary>
        /// 验证码
        /// </summary>
        Code,

        /// <summary>
        /// 短信通知
        /// </summary>
        Message,

        /// <summary>
        /// 营销短信
        /// </summary>
        MarkingMessage
    }
}