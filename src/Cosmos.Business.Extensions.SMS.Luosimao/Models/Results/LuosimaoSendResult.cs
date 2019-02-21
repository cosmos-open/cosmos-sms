using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.Luosimao.Models.Results
{
    public class LuosimaoSendResult
    {
        [JsonProperty("error")]
        public int Error { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}