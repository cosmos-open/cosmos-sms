using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results
{
    public class SimpleResponseData:ResponseData
    {
        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
