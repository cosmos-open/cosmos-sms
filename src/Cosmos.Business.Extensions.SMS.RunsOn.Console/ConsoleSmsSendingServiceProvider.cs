using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Core.Aggregation;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmos.Business.Extensions.SMS.RunsOn.Console {
    public class ConsoleSmsSendingServiceProvider : ISmsSendingServiceProvider {
        private readonly IServiceProvider _provider;
        private readonly ISmsAggregationSender _aggregationSender;
        private readonly SmsBizPkgConfiguration _businessPackageConfiguration;

        public ConsoleSmsSendingServiceProvider(IServiceProvider provider, SmsBizPkgConfiguration configuration) {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _aggregationSender = _provider.GetService<ISmsAggregationSender>() ?? throw new ArgumentNullException(nameof(ISmsAggregationSender));
            _businessPackageConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ISmsSender GetSmsSender(List<string> specificImplementList = null) {
            return new ConsoleSmsSender(_aggregationSender, _businessPackageConfiguration, specificImplementList);
        }
    }
}