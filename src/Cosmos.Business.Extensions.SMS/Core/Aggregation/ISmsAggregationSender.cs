using System.Threading;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Events;

namespace Cosmos.Business.Extensions.SMS.Core.Aggregation {
    public interface ISmsAggregationSender {
        Task SendAsync(SmsSendEvent sendEvent, CancellationToken cancellationToken = default(CancellationToken));
    }
}