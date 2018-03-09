using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Weimi.Configuration;
using Cosmos.Business.Extensions.SMS.Weimi.Core;
using Cosmos.Business.Extensions.SMS.Weimi.Models;
using Cosmos.Business.Extensions.SMS.Weimi.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.Weimi {
    public class WeimiSmsClient {
        private readonly WeimiSmsConfig _config;
        private readonly WeimiSmsAccount _weimiSmsAccount;
        private readonly IWeimiSmsApi _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public WeimiSmsClient(WeimiSmsConfig config, Action<Exception> exceptionHandler = null) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _weimiSmsAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));

            _proxy = config.Security
                ? HttpApiClient.Create<IWeimiSmsApi>("https://api.weimi.cc")
                : HttpApiClient.Create<IWeimiSmsApi>("http://api.weimi.cc");

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<WeimiSmsResult> SendAsync(WeimiSmsMessage message) {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_weimiSmsAccount.Uid)) throw new ArgumentNullException(nameof(_weimiSmsAccount.Uid));
            if (string.IsNullOrWhiteSpace(_weimiSmsAccount.Pas)) throw new ArgumentNullException(nameof(_weimiSmsAccount.Pas));

            message.CheckParameters();

            var bizParams = new Dictionary<string, string> {
                {"uid", _weimiSmsAccount.Uid},
                {"pas", _weimiSmsAccount.Pas},
                {"mob", message.GetPhoneString()},
                {"con", message.Content},
                {"type", "json"}
            };
            if (!string.IsNullOrWhiteSpace(message.Timing))
                bizParams.Add("timing", message.Timing);

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendMessageAsync(content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        public async Task<WeimiSmsResult> SendCodeAsync(WeimiSmsCode code) {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_weimiSmsAccount.Uid)) throw new ArgumentNullException(nameof(_weimiSmsAccount.Uid));
            if (string.IsNullOrWhiteSpace(_weimiSmsAccount.Pas)) throw new ArgumentNullException(nameof(_weimiSmsAccount.Pas));

            code.CheckParameters();

            var bizParams = new Dictionary<string, string>(code.GetTemplateParams()) {
                {"uid", _weimiSmsAccount.Uid},
                {"pas", _weimiSmsAccount.Pas},
                {"mob", code.GetPhoneString()},
                {"cid", code.TemplateId},
                {"type", "json"}
            };
            if (!string.IsNullOrWhiteSpace(code.Timing))
                bizParams.Add("timing", code.Timing);

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendMessageAsync(content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }


        private static WeimiSmsResult ReturnAsDefautlResponse()
            => new WeimiSmsResult {
                Code = -500,
                Message = "解析错误，返回默认结果"
            };
    }
}