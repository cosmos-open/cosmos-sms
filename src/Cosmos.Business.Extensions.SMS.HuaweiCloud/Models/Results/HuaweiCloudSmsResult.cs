using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Models.Results
{
    /*
     * 参数及错误码参考：
     * https://support.huaweicloud.com/api-msgsms/sms_05_0001.html
     */

    public class HuaweiCloudSmsResult
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public List<HuaweiCloudSmsID> Result { get; set; }

        public bool IsSuccess()
        {
            return Code == "000000";
        }
    }

    public class HuaweiCloudSmsID
    {
        public string SmsMsgId { get; set; }

        public string From { get; set; }

        public string OriginTo { get; set; }

        public string Status { get; set; }

        public string CreateTime { get; set; }
    }
}