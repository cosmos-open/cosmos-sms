using System;
using System.Globalization;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions {
    public static class DateTimeFormatExtensions {
        private const string ISO8601_DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ss'Z'";

        public static string ToIso8601DateString(this DateTime dt) {
            return dt.ToUniversalTime().ToString(ISO8601_DATE_FORMAT, CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}