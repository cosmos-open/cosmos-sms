using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Core {
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class DynamicHttpPostAttribute : Attribute, IApiParameterAttribute {
        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter) {

            if (parameter.Value is string path) {
                var baseUri = context.RequestMessage.RequestUri;
                var relative = string.IsNullOrEmpty(path) ? null : new Uri(path, UriKind.RelativeOrAbsolute);
                var requestUri = GetRequestUri(baseUri, relative);
                context.RequestMessage.RequestUri = requestUri;
            }

            return Task.CompletedTask;
        }

        private Uri GetRequestUri(Uri baseUri, Uri relative) {
            if (baseUri == null) {
                if (relative == null || relative.IsAbsoluteUri == true) {
                    return relative;
                }

            } else {
            } else {
                if (relative == null) {
                    return baseUri;
                }

                return new Uri(baseUri, relative);
            }
        }
    }
}