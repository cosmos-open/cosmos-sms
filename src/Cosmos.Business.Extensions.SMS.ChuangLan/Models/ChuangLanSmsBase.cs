using System;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Models
{
    public class ChuanglanSmsBase
    {
        public DateTime? SendTime { get; set; }

        public bool? Report { get; set; } = false;

        public string Extend { get; set; }

        public string Uid { get; set; }

    }
}