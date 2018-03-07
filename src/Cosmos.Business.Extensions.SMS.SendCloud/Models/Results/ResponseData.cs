using Cosmos.Business.Extensions.SMS.SendCloud.Core.Extensions;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Models.Results {
    public class ResponseData<TData> {
        public bool Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TData Info { get; set; }

        public string ToJsonString() {
            return this.ToJson();
        }
    }
}