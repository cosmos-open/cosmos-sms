using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS.Configuration
{
    public interface ISmsOptions
    {
        List<string> SpecificImplementList { get; set; }
    }
}