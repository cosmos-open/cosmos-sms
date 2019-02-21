using System;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Models.Results
{
    public class AliyunDysmsResult
    {
        public string Recommend { get; set; }

        public string RequestId { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string BizId { get; set; }

        public bool IsSuccess()
        {
            return string.Compare(Code, "ok", StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}