using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Yunpian.Configuration;
using Cosmos.Business.Extensions.SMS.Yunpian.Models;
using Yunpian.Sdk;
using Yunpian.Sdk.Model;

namespace Cosmos.Business.Extensions.SMS.Yunpian
{
    public class YunpianSmsClient : IDisposable
    {
        private readonly YunpianConfig _config;
        private readonly YunpianAccount _yunpianAccount;
        private YunpianClient _proxy { get; set; }
        private readonly Action<Exception> _exceptionHandler;

        public YunpianSmsClient(YunpianConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _yunpianAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));
            _proxy = new YunpianClient(_yunpianAccount.ApiKey).Init();

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public Task<Result<SmsBatchSend>> SendAsync(YunpianSmsMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_yunpianAccount.ApiKey)) throw new ArgumentNullException(nameof(_yunpianAccount.ApiKey));

            message.CheckParameters();

            var bizParams = new Dictionary<string, string> {
                {Const.Mobile, message.GetPhoneString()},
                {Const.Text, message.Content}
            };

            if (!string.IsNullOrWhiteSpace(message.Extend))
                bizParams.Add(Const.Extend, message.Extend);

            if (!string.IsNullOrWhiteSpace(_config.CallbackUrl))
                bizParams.Add(Const.CallbackUrl, _config.CallbackUrl);

            var response = _proxy.Sms().BatchSend(bizParams);

            return Task.FromResult(response);
        }

        public Task<Result<SmsSingleSend>> SendCodeAsync(YunpianSmsCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_yunpianAccount.ApiKey)) throw new ArgumentNullException(nameof(_yunpianAccount.ApiKey));

            code.CheckParameters();

            var bizParams = new Dictionary<string, string> {
                {Const.Mobile, code.PhoneNumber},
                {Const.Text, code.Content}
            };

            if (!string.IsNullOrWhiteSpace(code.Extend))
                bizParams.Add(Const.Extend, code.Extend);

            if (!string.IsNullOrWhiteSpace(code.Uid))
                bizParams.Add(Const.Uid, code.Uid);

            if (!string.IsNullOrWhiteSpace(_config.CallbackUrl))
                bizParams.Add(Const.CallbackUrl, _config.CallbackUrl);

            if (code.Register.HasValue)
                bizParams.Add(Const.Register, code.Register.Value ? "true" : "false");

            var response = _proxy.Sms().SingleSend(bizParams);

            return Task.FromResult(response);
        }

        public void Dispose()
        {
            _proxy?.Dispose();
        }
    }
}