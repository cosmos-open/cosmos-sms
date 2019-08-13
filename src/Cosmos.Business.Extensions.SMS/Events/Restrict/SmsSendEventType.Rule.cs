using System;

namespace Cosmos.Business.Extensions.SMS.Events.Restrict
{
    public class SmsSendEventRule
    {
        public SmsSendEventRule(int maxReceivers)
        {
            if (maxReceivers <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxReceivers), maxReceivers, "Max reseivers limited must larger than zero.");
            MaxReceivers = maxReceivers;
        }

        public int MaxReceivers { get; }
    }
}