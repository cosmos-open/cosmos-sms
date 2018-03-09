using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions
{
    public static class StringExtensions{
        public static byte[] HexToBytes(this string value) {
            int index = 0;
            List<byte> list = new List<byte>(value.Length / 2);

            while (index< value.Length)
            {
                list.Add(byte.Parse(value.Substring(index, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                index += 2;
            }

            return list.ToArray();
        }
    }
}