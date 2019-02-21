using System;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core.Helpers
{
    /// <summary>
    /// UUID version 4
    /// see more:
    ///     https://en.wikipedia.org/wiki/Universally_unique_identifier
    ///     https://www.uuidgenerator.net/version4
    /// </summary>
    public static class UUID
    {
        public static string CreateAsString() => Guid.NewGuid().ToString();
    }
}