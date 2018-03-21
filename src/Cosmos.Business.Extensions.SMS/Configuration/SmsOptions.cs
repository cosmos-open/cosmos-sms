using System.Collections.Generic;

namespace Cosmos.Business.Extensions.SMS.Configuration {
    public class SmsOptions : ISmsOptions {
        public List<string> SpecificImplementList { get; set; }
    }
}