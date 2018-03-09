using System;

namespace Cosmos.Business.Extensions.SMS.Core.SmsImplements {
    public interface ISmsImplementClientProvider<in TImplementConfig, TImplementSendCalledResultWrapper>
        where TImplementConfig : class, IConfig, new() {
        ISmsImplementClient<TImplementSendCalledResultWrapper> GetClient(TImplementConfig config, Action<Exception> exceptionHandler = null);
    }
}