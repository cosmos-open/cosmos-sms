using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Core.Aggregation;

namespace Cosmos.Business.Extensions.SMS.RunsOn.AspNetCore
{
    public class AspNetCoreSmsSender : SmsSenderBase
    {
        public AspNetCoreSmsSender(ISmsAggregationSender sender, SmsBizPkgConfiguration configuration, List<string> specificImplementList = null)
            : base(sender, configuration, specificImplementList) { }
    }
}