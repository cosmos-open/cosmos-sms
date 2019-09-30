using JDCloudSDK.Sms.Apis;

namespace Cosmos.Business.Extensions.SMS.JdCloud.Models.Results
{
    public class JdCloudSmsResult
    {
        public JdCloudSmsResult(BatchSendResponse response)
        {
            RequestId = response.RequestId;

            if (response.Result != null)
            {
                SequenceNumber = response.Result.Data?.SequenceNumber;
                Status = response.Result.Status;
                Code = response.Result.Code;
                Message = response.Result.Message;
            }
        }

        public string RequestId { get; set; }

        /// <summary>本次发送请求的序列号</summary>
        public string SequenceNumber { get; set; }

        /// <summary>请求状态</summary>
        public bool Status { get; set; }

        /// <summary>错误码</summary>
        public string Code { get; set; }

        /// <summary>错误消息</summary>
        public string Message { get; set; }

        public bool IsSuccess()=> Status && Code == "200";
    }
}