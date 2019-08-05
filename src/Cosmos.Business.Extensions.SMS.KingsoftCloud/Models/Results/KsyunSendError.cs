namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Models.Results
{
    /// <summary>
    /// Kingsoft Cloud SMS Send Error
    /// <br />
    /// Error list: https://docs.ksyun.com/documents/6481?preview=1
    /// </summary>
    public class KsyunSendError
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}