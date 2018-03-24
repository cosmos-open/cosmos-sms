using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core {
    public static class JsonExtensions {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj);
        }
    }
}