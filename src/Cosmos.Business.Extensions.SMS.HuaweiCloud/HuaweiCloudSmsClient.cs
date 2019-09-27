using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Configuration;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Core;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Core.Helpers;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Models;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud
{
    public class HuaweiCloudSmsClient : SmsClientBase
    {
        private readonly HuaweiCloudSmsConfig _config;
        private readonly HuaweiCloudAccount _huaweiCloudAccount;
        private readonly IHuaweiCloudSmsApis _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public HuaweiCloudSmsClient(HuaweiCloudSmsConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _huaweiCloudAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));
            _proxy = WebApiClientCreator.Create(_config);

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<HuaweiCloudSmsResult> SendAsync(HuaweiCloudSmsMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_huaweiCloudAccount.AppKey)) throw new ArgumentNullException(nameof(_huaweiCloudAccount.AppKey));
            if (string.IsNullOrWhiteSpace(_huaweiCloudAccount.AppSecret)) throw new ArgumentNullException(nameof(_huaweiCloudAccount.AppSecret));

            message.CheckParameters();

            var bizParams = new Dictionary<string, string>
            {
                {"from", _config.Sender},
                {"to", message.PhoneNumberList.ToReceiver()},
                {"templateId", _config.TemplateId},
                {"templateParas", message.Params.ToTemplateParas()},
                {"statusCallback", _config.StatusCallBackUri},
                {"signature", _config.Signature}
            };

            var content = new FormUrlEncodedContent(bizParams);
            var wsseObj = new WsseObject(_huaweiCloudAccount);

            return await _proxy.SendMessageAsync(content, wsseObj)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        public async Task<HuaweiCloudSmsResult> SendCodeAsync(HuaweiCloudSmsCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_huaweiCloudAccount.AppKey)) throw new ArgumentNullException(nameof(_huaweiCloudAccount.AppKey));
            if (string.IsNullOrWhiteSpace(_huaweiCloudAccount.AppSecret)) throw new ArgumentNullException(nameof(_huaweiCloudAccount.AppSecret));

            code.CheckParameters();

            var bizParams = new Dictionary<string, string>
            {
                {"from", _config.Sender},
                {"to", code.PhoneNumberList.ToReceiver()},
                {"templateId", _config.TemplateId},
                {"templateParas", code.Params.ToTemplateParas()},
                {"statusCallback", _config.StatusCallBackUri},
                {"signature", _config.Signature}
            };

            var content = new FormUrlEncodedContent(bizParams);
            var wsseObj = new WsseObject(_huaweiCloudAccount);

            return await _proxy.SendCodeAsync(content, wsseObj)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        private static HuaweiCloudSmsResult ReturnAsDefaultResponse()
            => new HuaweiCloudSmsResult
            {
                Code = "-500",
                Description = "解析错误，返回默认结果"
            };
        
        public override void CheckMyself() { }
    }
}