using System;
using System.Threading.Tasks;
using WebApiClient.Contexts;
using WebApiClient.Interfaces;

namespace Cosmos.Business.Extensions.SMS.Luosimao.Core {
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class HttpHeaderFieldAttribute : Attribute, IApiParameterAttribute {

        public string Key { get; set; }

        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter) {
            var name = string.IsNullOrWhiteSpace(Key) ? parameter.Name : Key;

            if (parameter.Value is string stringVal) {
                context.RequestMessage.Headers.TryAddWithoutValidation(name, stringVal);
            }

            return Task.CompletedTask;
        }
    }
}