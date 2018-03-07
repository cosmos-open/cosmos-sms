using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Business.Extensions.SMS.Abstractions;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Configuration
{
    public class ChuangLanAccount:IAccountSettings
    {
        public string User { get; set; }
        public string Key { get; set; }

        public string ApiUrl { get; set; }
    }
}
