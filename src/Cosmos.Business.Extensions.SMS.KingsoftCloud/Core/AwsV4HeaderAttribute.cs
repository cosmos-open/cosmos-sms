using System;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contexts;

namespace Cosmos.Business.Extensions.SMS.KingsoftCloud.Core
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class AwsV4HeaderAttribute : Attribute, IApiParameterAttribute
    {

        public Task BeforeRequestAsync(ApiActionContext context, ApiParameterDescriptor parameter)
        {
            if (parameter.Value is AwsV4.SignedResult signer)
            {
                context.RequestMessage.Headers.TryAddWithoutValidation("authorization", signer.Authorization);
                context.RequestMessage.Headers.TryAddWithoutValidation("x-amz-date", signer.Time);
            }

            return Task.CompletedTask;
        }
    }
}