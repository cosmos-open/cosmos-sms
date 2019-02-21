using qcloudsms_csharp;

namespace Cosmos.Business.Extensions.SMS.TencentCloud.Core
{
    public class TencentSmsSenderProxy
    {
        private readonly int _appId;

        private readonly string _appKey;

        private SmsSingleSender SingleSenderCache { get; set; }
        private SmsMultiSender MultiSenderCache { get; set; }

        public TencentSmsSenderProxy(int appId, string appKey)
        {
            _appId = appId;
            _appKey = appKey;
        }

        private bool HasInitSingleSender() => SingleSenderCache != null;

        public bool HasInitMultiSender() => MultiSenderCache != null;

        public SmsSingleSender GetSingleSender()
        {
            if (!HasInitSingleSender())
            {
                SingleSenderCache = new SmsSingleSender(_appId, _appKey);
            }

            return SingleSenderCache;
        }

        public SmsMultiSender GetMultiSender()
        {
            if (!HasInitMultiSender())
            {
                MultiSenderCache = new SmsMultiSender(_appId, _appKey);
            }

            return MultiSenderCache;
        }

    }
}