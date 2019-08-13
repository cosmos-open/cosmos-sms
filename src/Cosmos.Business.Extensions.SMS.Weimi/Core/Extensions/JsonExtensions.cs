using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.Weimi.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}