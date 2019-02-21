using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS.Core.SmsImplements
{
    public interface ISmsImplementRoller
    {
        string GetRendomImplement(List<string> finalServiceNames);
    }
}