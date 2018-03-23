using System;
using System.Threading.Tasks;
using WebApiClient.Contexts;
using WebApiClient.Interfaces;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Core {
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class AuthorizationAttribute : Attribute, IApiParameterAttribute {
        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter) {

            if (parameter.Value is string authStr) {
                context.RequestMessage.Headers.TryAddWithoutValidation("Authorization", authStr);
            }

            return Task.CompletedTask;
        }
    }
}