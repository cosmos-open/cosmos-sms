using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
