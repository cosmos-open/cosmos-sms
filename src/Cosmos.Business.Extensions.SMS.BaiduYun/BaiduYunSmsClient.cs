using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.BaiduYun.Configuration;
using Cosmos.Business.Extensions.SMS.BaiduYun.Core;
using Cosmos.Business.Extensions.SMS.BaiduYun.Core.Helpers;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models.Results;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.BaiduYun
{
    public class BaiduYunSmsClient : SmsClientBase
    {
        private readonly BaiduYunConfig _config;
        private readonly BaiduYunAccount _account;
        private readonly IBaiduYunSmsApis _proxy;
        private readonly string _apiServerUrl;
        private readonly Action<Exception> _exceptionHandler;

        public BaiduYunSmsClient(BaiduYunConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _account = _config.Account ?? throw new ArgumentNullException(nameof(_config.Account));
            _apiServerUrl = ApiAddressHelper.Get(config);
            _proxy = WebApiClientCreator.Create(_apiServerUrl, _config.TimeOut);

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<BaiduYunSmsResult> SendAsync(BaiduYunMessage message)
        {

            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_account.AccessKeyId)) throw new ArgumentNullException(nameof(_account.AccessKeyId));
            if (string.IsNullOrWhiteSpace(_account.SecretAccessKey)) throw new ArgumentNullException(nameof(_account.SecretAccessKey));
            if (string.IsNullOrWhiteSpace(_config.InvokeId)) throw new ArgumentNullException(nameof(_config.InvokeId));

            message.CheckParameters();

            var bizParams = message.ToSendObject(_config);
            var bceWrapper = new BceObjectWrapper(new BceObject(_config), message, _config) {ApiServerUrl = _apiServerUrl};

            return await _proxy.SendAsync(bceWrapper, bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        public async Task<BaiduYunSmsResult> SendCodeAsync(BaiduYunCode code)
        {

            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_account.AccessKeyId)) throw new ArgumentNullException(nameof(_account.AccessKeyId));
            if (string.IsNullOrWhiteSpace(_account.SecretAccessKey)) throw new ArgumentNullException(nameof(_account.SecretAccessKey));
            if (string.IsNullOrWhiteSpace(_config.InvokeId)) throw new ArgumentNullException(nameof(_config.InvokeId));

            code.CheckParameters();

            if (!code.Vars.ContainsKey(code.CodeKey))
                code.Vars.Add(code.CodeKey, code.Code);

            var bizParams = code.ToSendObject(_config);
            var bceWrapper = new BceObjectWrapper(new BceObject(_config), code, _config) {ApiServerUrl = _apiServerUrl};

            return await _proxy.SendAsync(bceWrapper, bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        private static BaiduYunSmsResult ReturnAsDefaultResponse()
            => new BaiduYunSmsResult
            {
                Code = 500,
                Message = "解析错误，返回默认结果"
            };
        
        public override void CheckMyself() { }
    }
}