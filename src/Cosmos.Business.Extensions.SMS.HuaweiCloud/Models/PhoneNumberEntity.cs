namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Models
{
    public struct PhoneNumberEntity
    {
        public PhoneNumberEntity(string countryCode, string phoneNumber)
        {
            CountryCode = countryCode;
            PhoneNumber = phoneNumber;
        }

        public string CountryCode { get; }

        public string PhoneNumber { get; }

        public override string ToString() => $"+{CountryCode}{PhoneNumber}";

        public int Length => CountryCode.Length + PhoneNumber.Length + 1;

        public bool IsValid()
        {
            //每个号码的长度不得大于 21 位
            return Length <= 21;
        }
    }
}