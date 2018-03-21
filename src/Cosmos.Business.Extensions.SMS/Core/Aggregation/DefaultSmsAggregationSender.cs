using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Core.SmsImplements;
using Cosmos.Business.Extensions.SMS.Events;

namespace Cosmos.Business.Extensions.SMS.Core.Aggregation {
    public class DefaultSmsAggregationSender : ISmsAggregationSender {
        private readonly ISmsImplementRoller _roller;

        public DefaultSmsAggregationSender(ISmsImplementRoller implementRoller) {
            _roller = implementRoller ?? throw new ArgumentNullException(nameof(implementRoller));
        }

        public async Task SendAsync(SmsSendEvent sendEvent, CancellationToken cancellationToken = default(CancellationToken)) {
            //todo 根据 more info 获得优先实现名单（如果没有名单，则从注册清单中获得实现名单）

            //todo 根据实现名单、约束规则和 send event 自身属性，获得最佳实现名单
            List<string> finalServiceNames = new List<string>();

            if (finalServiceNames == null || !finalServiceNames.Any()) {
                //todo log error info 
                return;
            }

            //todo 如果最佳实现名单只有一个实现，直接调用 
            if (finalServiceNames.Count == 1) {
                //todo get implement func from implement manager by name
                return;
            }

            //todo 如果最佳实现名单为复数个实现，则调用 Roller 获得随机的实现
            var finalServiceName = _roller.GetRendomImplement(finalServiceNames);

            //todo 从 implement client provider 中获得 implement client
            //todo get implement func from implement manager by name

            //todo 调用 implement client 的指定方法（模板方法、Code 方法 还是一般消息方法） 
            //todo use implementFunc.Invoke(sendEvent) send message and await
        }
    }
}