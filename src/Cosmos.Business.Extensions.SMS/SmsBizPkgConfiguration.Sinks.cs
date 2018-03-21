using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Configuration;
using Microsoft.Extensions.Configuration;

namespace Cosmos.Business.Extensions.SMS {
    public partial class SmsBizPkgConfiguration {
        private readonly Dictionary<string, SinkConfiguration> _sinkConfigurations = new Dictionary<string, SinkConfiguration>();
        private readonly object _sinkConfigurationsLock = new object();

        public void SetSinkConfiguration<TSinkConfiguration, TSinkSettings>(string sectionName, TSinkSettings settings,
            Action<IConfiguration, TSinkConfiguration> addtionalAct = null)
            where TSinkConfiguration : SinkConfiguration, new() where TSinkSettings : class, ISmsSinkOptions, new() {
            if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentNullException(nameof(sectionName));
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            var section = _configurationRoot.GetSection($"Logging:{sectionName}");
            var configuration = section.Get<TSinkConfiguration>() ?? new TSinkConfiguration();
            addtionalAct?.Invoke(section, configuration);

            SetSinkConfiguration(configuration, settings);
        }

        public void SetSinkConfiguration<TSinkConfiguration, TSinkSettings>(TSinkConfiguration configuration, TSinkSettings settings)
            where TSinkConfiguration : SinkConfiguration, new() where TSinkSettings : class, ISmsSinkOptions, new() {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (string.IsNullOrWhiteSpace(configuration.Name)) throw new ArgumentNullException(nameof(configuration.Name));
            if (!_sinkConfigurations.ContainsKey(configuration.Name)) {
                lock (_sinkConfigurationsLock) {
                    if (!_sinkConfigurations.ContainsKey(configuration.Name)) {
                        //todo 预处理
                        _sinkConfigurations.Add(configuration.Name, configuration);
                    }
                }
            }
        }

        public TSinkConfiguration GetSinkConfiguration<TSinkConfiguration>() where TSinkConfiguration : SinkConfiguration, new() {
            var temp = new TSinkConfiguration();
            return GetSinkConfiguration<TSinkConfiguration>(temp.Name);
        }

        public TSinkConfiguration GetSinkConfiguration<TSinkConfiguration>(string name) where TSinkConfiguration : SinkConfiguration, new() {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            return _sinkConfigurations[name] as TSinkConfiguration;
        }

        public SinkConfiguration GetSinkConfiguration(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            return _sinkConfigurations[name];
        }
    }
}