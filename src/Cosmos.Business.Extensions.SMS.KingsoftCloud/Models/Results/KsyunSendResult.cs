namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Models.Results
{
    /// <summary>
    /// Kingsoft Cloud SMS Send Result
    /// </summary>
    public class KsyunSendResult
    {
        /// <summary>
        /// Request Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public KsyunSendError Error { get; set; }

        /// <summary>
        /// Is success
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess() => Error == null;
    }
}