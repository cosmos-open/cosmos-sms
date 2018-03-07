using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results
{
    public class VariableResponseData:ResponseData
    {
        [JsonProperty("successNum")]
        public string SuccessCount { get; set; }

        [JsonProperty("failNum")]
        public string FailedCount { get; set; }
    }
}
