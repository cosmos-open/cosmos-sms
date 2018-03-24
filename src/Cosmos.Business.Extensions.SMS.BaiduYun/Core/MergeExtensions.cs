using System.Collections.Generic;
using System.Text;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Core {
    public static class MergeExtensions {
        public static string Merge(this IEnumerable<string> stringList, string str) {
            var sb = new StringBuilder();
            foreach (var item in stringList) {
                sb.Append($"{item}{str}");
            }

            return sb.ToString(0, sb.Length - str.Length);
        }
    }
}