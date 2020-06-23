using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Configuration;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Core;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Models;
using Cosmos.Business.Extensions.SMS.KingsoftCloud.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.KingsoftCloud
{
    public class KsyunSmsClient : SmsClientBase
    {
        private readonly KsyunConfig _config;
        private readonly KsyunAccount _account;
        protected readonly IKsyunSmsApis _proxy;
        private readonly string _apiServerUrl;
        private readonly Action<Exception> _exceptionHandler;

        public KsyunSmsClient(KsyunConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _account = _config.Account ?? throw new ArgumentNullException(nameof(_config.Account));
            _apiServerUrl = $"{GetHttpPrefix(config)}://{KsyunSmsConstants.Host}";
            _proxy = HttpApi.Create<IKsyunSmsApis>(_apiServerUrl);

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<KsyunSendResult> SendAsync(KsyunSmsMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_account.AccessKeyId)) throw new ArgumentNullException(nameof(_account.AccessKeyId));
            if (string.IsNullOrWhiteSpace(_account.AccessKeySecret)) throw new ArgumentNullException(nameof(_account.AccessKeySecret));

            message.CheckParameters();

            message.SignName = _config.SignName;

            var paramsObj = message.ToRequestObject();
            var content = new FormUrlEncodedContent(paramsObj);
            var sign = message.GetSignedResult(_config, content);

            return await _proxy.SendAsync(sign, content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        public async Task<KsyunSendResult> SendCodeAsync(KsyunSmsCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_account.AccessKeyId)) throw new ArgumentNullException(nameof(_account.AccessKeyId));
            if (string.IsNullOrWhiteSpace(_account.AccessKeySecret)) throw new ArgumentNullException(nameof(_account.AccessKeySecret));

            code.CheckParameters();

            code.SignName = _config.SignName;

            var paramsObj = code.ToRequestObject();
            var content = new FormUrlEncodedContent(paramsObj);
            var sign = code.GetSignedResult(_config, content);

            return await _proxy.SendAsync(sign, content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        private static KsyunSendResult ReturnAsDefaultResponse()
            => new KsyunSendResult
            {
                Error = new KsyunSendError
                {
                    Type = "Unknow",
                    Message = "解析错误，返回默认结果",
                    Code = "500"
                },
            };

        private static string GetHttpPrefix(KsyunConfig config)
        {
            if (config == null || !config.Security)
            {
                return "http";
            }

            return "https";
        }

        public override void CheckMyself() { }
    }
}