using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.RongCloud.Models;
using WebApiClient;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Core
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class SignatureBagAttribute : Attribute, IApiParameterAttribute
    {
        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter)
        {

            if (parameter.Value is RongCloudSignatureBag bag)
            {
                context.RequestMessage.Headers.TryAddWithoutValidation("App-Key", bag.AppKey);
                context.RequestMessage.Headers.TryAddWithoutValidation("Nonce", bag.Nonce);
                context.RequestMessage.Headers.TryAddWithoutValidation("Timestamp", bag.Timestamp);
                context.RequestMessage.Headers.TryAddWithoutValidation("Signature", bag.Signature);
            }

            return Task.CompletedTask;
        }
    }
}