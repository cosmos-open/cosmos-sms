using System.Threading.Tasks;

namespace Cosmos.Business.Extensions.SMS {
    public interface ISMSClient<in TSmsMessage, in TSmsCode, TSmsCalledResultWrapper> {
        Task<TSmsCalledResultWrapper> SendAsync(TSmsMessage message);
        Task<TSmsCalledResultWrapper> SendCodeAsync(TSmsCode code);
    }
}