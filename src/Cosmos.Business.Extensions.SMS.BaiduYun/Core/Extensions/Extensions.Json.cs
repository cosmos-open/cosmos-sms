using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    }
}