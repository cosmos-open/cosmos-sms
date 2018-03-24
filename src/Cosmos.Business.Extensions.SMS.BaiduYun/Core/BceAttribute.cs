using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models;
using Cosmos.Encryption;
using WebApiClient.Contexts;
using WebApiClient.Interfaces;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core {
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class BceAttribute : Attribute, IApiParameterAttribute {
        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter) {

            if (parameter.Value is BceObjectWrapper wrapper) {
                var date = DateTime.UtcNow;
                var canonicalRequest = $"POST\n/{WebUtility.UrlEncode(wrapper.UrlSegment)}\n\nhost:{WebUtility.UrlEncode(wrapper.ApiServerUrl)}";
                //var canonicalRequest = $"POST\n/{(wrapper.UrlSegment)}\n\nhost:{(wrapper.ApiServerUrl)}";
                var content = wrapper.Message.ToSendObject(wrapper.Config).ToJson();

                context.RequestMessage.Timeout = TimeSpan.FromMilliseconds(wrapper.Config.TimeOut);
                context.RequestMessage.Headers.TryAddWithoutValidation("x-bce-date", date.ToString("YYYY-MM-DD"));
                context.RequestMessage.Headers.TryAddWithoutValidation("Authorization", wrapper.BceObject.GetAuthString(date, canonicalRequest, "host"));
                context.RequestMessage.Headers.TryAddWithoutValidation("x-bce-content-sha256", SHA256HashingProvider.Signature(content, Encoding.UTF8));
            }

            return Task.CompletedTask;
        }
    }
}