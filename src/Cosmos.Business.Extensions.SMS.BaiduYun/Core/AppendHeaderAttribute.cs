using System;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AppendHeaderAttribute : ApiActionAttribute {

        public override Task BeforeRequestAsync(ApiActionContext context) {
            context.RequestMessage.Headers.TryAddWithoutValidation("ContentType", "application/json;charset=utf-8");
            return Task.CompletedTask;
        }
    }
}