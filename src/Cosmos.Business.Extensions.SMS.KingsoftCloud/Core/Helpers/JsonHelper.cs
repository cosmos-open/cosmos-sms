namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Core.Helpers
{
    internal static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}