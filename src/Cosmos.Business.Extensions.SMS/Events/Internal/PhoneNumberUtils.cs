using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Business.Extensions.SMS.Events.Internal
{
    internal static class PhoneNumberUtils
    {
        public static List<(string, string)> CreatePhoneNumberTuple(string nationCode, List<string> phoneNumbers)
        {
            return phoneNumbers.Select(number => (nationCode, number)).ToList();
        }

        public static List<(string, string)> CreatePhoneNumberTuple(string nationCode, string phoneNumber)
        {
            return new List<(string, string)> { (nationCode, phoneNumber) };
        }
    }
}