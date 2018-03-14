using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.RongCloud.Configuration;
using Cosmos.Business.Extensions.SMS.RongCloud.Core;
using Cosmos.Business.Extensions.SMS.RongCloud.Models;
using Cosmos.Business.Extensions.SMS.RongCloud.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.RongCloud {
    public class RongCloudClient {
        private readonly RongCloudConfig _config;
        private readonly RongCloudAccount _rongAccount;
        private readonly IRongCloudSmsApis _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public RongCloudClient(RongCloudConfig config, Action<Exception> exceptionHandler = null) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _rongAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));
            _proxy = HttpApiClient.Create<IRongCloudSmsApis>();

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<RongCloudSmsResult> SendCodeAsync(RongCloudSmsMessage message) {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_rongAccount.AppKey)) throw new ArgumentNullException(nameof(_rongAccount.AppKey));
            if (string.IsNullOrWhiteSpace(_rongAccount.AppSecret)) throw new ArgumentNullException(nameof(_rongAccount.AppSecret));

            message.CheckParameters();

            var bizParams = new Dictionary<string, string> {
                {"mobile", message.Mobile},
                {"templateId", message.TemplateId},
                {"region", message.Region}
            };

            foreach (var kvp in message.Vars) {
                bizParams.Add($"p{kvp.Key}", kvp.Value);
            }

            var signatureTuple = SignatureHalper.GenerateSignature(_rongAccount.AppSecret);
            var signatureBag = new RongCloudSignatureBag {
                AppKey = _rongAccount.AppKey,
                Nonce = signatureTuple.nonce,
                Signature = signatureTuple.signature,
                Timestamp = signatureTuple.timestamp
            };

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendSmsAsync(signatureBag, content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        private static RongCloudSmsResult ReturnAsDefautlResponse()
            => new RongCloudSmsResult {
                Code = 500,
                ErrorMessage = "解析错误，返回默认结果"
            };
    }
}