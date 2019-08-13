using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Events;

namespace Cosmos.Business.Extensions.SMS.Core.SmsImplements
{
    public interface ISmsImplementClient<TImplementSendCalledResultWrapper>
        where TImplementSendCalledResultWrapper : class, IImplementSendCalledResultWrapper, new()
    {
        Task<TImplementSendCalledResultWrapper> SendMessageAsync(SmsSendEvent sendEvent);
        Task<TImplementSendCalledResultWrapper> SendBatchMessageAsync(SmsSendEvent sendEvent);
        Task<TImplementSendCalledResultWrapper> SendCodeAsync(SmsSendEvent sendEvent);
        Task<TImplementSendCalledResultWrapper> SendBatchCodeAsync(SmsSendEvent sendEvent);
        Task<TImplementSendCalledResultWrapper> SendTemplateMessageAsync(SmsSendEvent sendEvent);
        Task<TImplementSendCalledResultWrapper> SendBatchTemplateMessageAsync(SmsSendEvent sendEvent);
    }
}