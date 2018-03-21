using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS {
    public interface ISmsSendingServiceProvider {
        ISmsSender GetSmsSender(List<string> specificImplementList = null);
    }
}