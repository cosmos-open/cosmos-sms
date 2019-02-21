using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    }
}
