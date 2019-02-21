using System;

namespace Cosmos.Business.Extensions.SMS.Core.RestrictContracts
{
    public class PhoneCountLimitedAttribute : Attribute
    {
        public int From { get; }
        public int To { get; }
        public bool OnlyOneLimited() => To - From == 0;

        public PhoneCountLimitedAttribute() : this(1, 1) { }

        public PhoneCountLimitedAttribute(int from = 1, int to = 1)
        {
            if (to < from) throw new ArgumentException("The number of to must greater than or equal to from.");
            if (from < 0) throw new ArgumentOutOfRangeException(nameof(from), "The number of from must greater than or equal to zero");
            if (to < 0) throw new ArgumentOutOfRangeException(nameof(to), "The number of to must greater than or equal to zero");

            From = from;
            To = to;
        }
    }
}