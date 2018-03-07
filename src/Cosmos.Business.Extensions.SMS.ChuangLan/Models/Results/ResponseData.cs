using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Business.Extensions.SMS.ChuangLan.Core.Extensions;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results
{
    public class ResponseData
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msgId")]
        public string MsgId { get; set; }

        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }

        

        public string ToJsonString()
        {
            return this.ToJson();
        }
    }
}
