using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Core.Extensions {
    public static class JsonExtensions {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj);
        }
    }
}