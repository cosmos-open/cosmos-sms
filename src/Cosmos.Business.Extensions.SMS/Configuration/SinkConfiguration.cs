using System;

namespace Cosmos.Business.Extensions.SMS.Configuration
{
    public abstract class SinkConfiguration
    {
        public readonly string Name;

        protected SinkConfiguration(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}