using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Configuration;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Helpers;
using WebApiClient;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class WsseAttribute : Attribute, IApiParameterAttribute
    {
        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter)
        {
            if (parameter.Value is HuaweiCloudAccount account)
            {
                context.RequestMessage.Headers.Add("X-WSSE", WsseHelper.BuildWSSEHeader(account.AppKey, account.AppSecret));
            }

            return Task.CompletedTask;
        }
    }
}