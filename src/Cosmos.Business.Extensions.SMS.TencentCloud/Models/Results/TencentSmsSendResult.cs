using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Business.Extensions.SMS.TencentCloud.Models.Results {
    public class TencentSmsSendResult {
        public int Result { get; set; }
        public string ErrMsg { get; set; }
        public string Sid { get; set; }
        public int Fee { get; set; }
        public string Mobile { get; set; }
        public string NationCode { get; set; }
    }

    public class TencentSmsSendResponseData {
        private int _feeInternal { get; set; }
        private string _sidInternal { get; set; }

        public int Result { get; set; }
        public string ErrMsg { get; set; }
        public string Ext { get; set; }

        public int Fee {
            get {
                if (Detail == null || !Detail.Any()) return _feeInternal;
                return Detail.Sum(x => x.Fee);
            }
            set {
                if (Detail == null || !Detail.Any()) _feeInternal = value;
            }
        }

        public string Sid {
            get {
                if (Detail == null || !Detail.Any()) return _sidInternal;
                return Detail.FirstOrDefault().Sid;
            }
            set {
                if (Detail == null || !Detail.Any()) _sidInternal = value;
            }
        }

        public List<TencentSmsSendResult> Detail { get; set; }
    }
}