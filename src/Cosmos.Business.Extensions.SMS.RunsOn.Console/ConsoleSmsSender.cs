using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Core.Aggregation;

namespace Cosmos.Business.Extensions.SMS.RunsOn.Console {
    public class ConsoleSmsSender : SmsSenderBase {
        public ConsoleSmsSender(ISmsAggregationSender sender, SmsBizPkgConfiguration configuration, List<string> specificImplementList = null)
            : base(sender, configuration, specificImplementList) { }
    }
}