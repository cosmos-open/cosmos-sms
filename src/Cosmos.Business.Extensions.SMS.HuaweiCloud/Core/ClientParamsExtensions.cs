using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Models;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Core
{
    internal static class ClientParamsExtensions
    {
        public static string ToReceiver(this IEnumerable<PhoneNumberEntity> phoneNumberEntities)
        {
            if (phoneNumberEntities == null)
                return string.Empty;

            var fen = "";
            var stringBuilder = new StringBuilder();
            foreach (var entity in phoneNumberEntities)
            {
                stringBuilder.Append(fen);
                fen = ",";

                stringBuilder.Append(entity.ToString());
            }

            return stringBuilder.ToString();
        }

        public static string ToTemplateParas(this List<string> originalParamsList)
        {
            if (originalParamsList == null || !originalParamsList.Any())
                return "";

            var fen = "";
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("[");

            foreach (var originalParams in originalParamsList)
            {
                stringBuilder.Append(fen);
                fen = ",";

                stringBuilder.Append($"\"{originalParams}\"");
            }

            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }
    }
}