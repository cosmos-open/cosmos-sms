using System.Threading;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Events;

namespace Cosmos.Business.Extensions.SMS.Core.Aggregation {
    public class DefaultSmsAggregationSender : ISmsAggregationSender {
        public async Task SendAsync(SmsSendEvent sendEvent, CancellationToken cancellationToken = default(CancellationToken)) {
            //todo 根据 more info 获得优先实现名单（如果没有名单，则从注册清单中获得实现名单）
            
            //todo 根据实现名单、约束规则和 send event 自身属性，获得最佳实现名单
            
            //todo 如果最佳实现名单只有一个实现，直接调用 
            
            //todo 如果最佳实现名单为复数个实现，则调用 Poller 获得随机的实现
            
            //todo 从 implement client provider 中获得 implement client
            
            //todo 调用 implement client 的指定方法（模板方法、Code 方法 还是一般消息方法） 
        }
    }
}