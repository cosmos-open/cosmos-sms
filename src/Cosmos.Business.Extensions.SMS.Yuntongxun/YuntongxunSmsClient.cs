using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Configuration;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Core;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Core.Helpers;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Models;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun
{
    public class YuntongxunSmsClient : SmsClientBase
    {
        private readonly YuntongxunSmsConfig _config;
        private readonly YuntongxunAccount _yuntongxunAccount;
        private readonly ICloopenApis _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public YuntongxunSmsClient(YuntongxunSmsConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _yuntongxunAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));

            _proxy = _config.Production
                ? HttpApi.Create<ICloopenApis>("https://app.cloopen.com:8883")
                : HttpApi.Create<ICloopenApis>("https://sandboxapp.cloopen.com:8883");

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<YuntongxunSmsResult> SendAsync(YuntongxunMessage message)
        {

            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_yuntongxunAccount.AccountSid)) throw new ArgumentNullException(nameof(_yuntongxunAccount.AccountSid));
            if (string.IsNullOrWhiteSpace(_yuntongxunAccount.AccountToken)) throw new ArgumentNullException(nameof(_yuntongxunAccount.AccountToken));
            if (string.IsNullOrWhiteSpace(_yuntongxunAccount.AppId)) throw new ArgumentNullException(nameof(_yuntongxunAccount.AppId));

            message.CheckParameters();

            var bizParams = message.ToSendObject(_yuntongxunAccount);

            var sigTuple = SignatureHelper.GetSignature(_yuntongxunAccount.AccountSid, _yuntongxunAccount.AccountToken);

            var urlSegment = $"/2013-12-26/Accounts/{_yuntongxunAccount.AccountSid}/SMS/TemplateSMS?sig={sigTuple.sig}";

            return await _proxy.SendAsync(urlSegment, sigTuple.auth, bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        public async Task<YuntongxunSmsResult> SendCodeAsync(YuntongxunCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_yuntongxunAccount.AccountSid)) throw new ArgumentNullException(nameof(_yuntongxunAccount.AccountSid));
            if (string.IsNullOrWhiteSpace(_yuntongxunAccount.AccountToken)) throw new ArgumentNullException(nameof(_yuntongxunAccount.AccountToken));
            if (string.IsNullOrWhiteSpace(_yuntongxunAccount.AppId)) throw new ArgumentNullException(nameof(_yuntongxunAccount.AppId));

            code.CheckParameters();

            var bizParams = code.ToSendObject(_yuntongxunAccount);

            var sigTuple = SignatureHelper.GetSignature(_yuntongxunAccount.AccountSid, _yuntongxunAccount.AccountToken);

            var urlSegment = $"/2013-12-26/Accounts/{_yuntongxunAccount.AccountSid}/SMS/TemplateSMS?sig={sigTuple.sig}";

            return await _proxy.SendAsync(urlSegment, sigTuple.auth, bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefaultResponse();
                });
        }

        private static YuntongxunSmsResult ReturnAsDefaultResponse()
            => new YuntongxunSmsResult
            {
                statusCode = "500",
                statusMsg = "解析错误，返回默认结果"
            };

        public override void CheckMyself() { }
    }
}