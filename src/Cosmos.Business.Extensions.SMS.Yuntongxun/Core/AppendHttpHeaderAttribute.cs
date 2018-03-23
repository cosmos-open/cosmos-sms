using System;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Core {
    
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AppendHttpHeaderAttribute : ApiActionAttribute{

        public override Task BeforeRequestAsync(ApiActionContext context) {
            context.RequestMessage.Headers.TryAddWithoutValidation("Accept", "application/json");
            context.RequestMessage.Headers.TryAddWithoutValidation("ContentType",  "application/json;charset=utf-8");
            return Task.CompletedTask;
        }
    }
}