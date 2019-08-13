using System;
using System.Collections.Generic;
using Cosmos.Business.Extensions.SMS.Configuration;
using Microsoft.Extensions.Configuration;

namespace Cosmos.Business.Extensions.SMS
{
    public partial class SmsBizPkgConfiguration
    {
        private readonly ISmsOptions _smsSettings;
        private readonly IConfigurationRoot _configurationRoot;

        public SmsBizPkgConfiguration() { }

        public SmsBizPkgConfiguration(ISmsOptions settings, IConfigurationRoot configurationRoot)
        {
            _smsSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _configurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
            SetSelf(configurationRoot.GetSection("SMS").Get<SmsBizPkgConfiguration>());
        }

        public IConfigurationRoot Configuration => _configurationRoot;

        public IReadOnlyList<string> GlobalSpecificImplementList => _smsSettings.SpecificImplementList;
    }
}