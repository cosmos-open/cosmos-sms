using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.Luosimao.Configuration;
using Cosmos.Business.Extensions.SMS.Luosimao.Core;
using Cosmos.Business.Extensions.SMS.Luosimao.Models;
using Cosmos.Business.Extensions.SMS.Luosimao.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.Luosimao {
    public class LuosimaoClient {
        private readonly LuosimaoConfig _config;
        private readonly LuosimaoAccount _luosimaoAccount;
        private readonly ILuosimaoApi _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public LuosimaoClient(LuosimaoConfig config, Action<Exception> exceptionHandler = null) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _luosimaoAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));
            _proxy = HttpApiClient.Create<ILuosimaoApi>();

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<LuosimaoSendResult> SendAsync(LuosimaoSmsMessage message) {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_luosimaoAccount.Password)) throw new ArgumentNullException(nameof(_luosimaoAccount.Password));
            if (string.IsNullOrWhiteSpace(_config.SignName)) throw new ArgumentNullException(nameof(_config.SignName));

            message.CheckParameters();

            var auth = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"api:key-{_luosimaoAccount.Password}"));

            var bizParams = new Dictionary<string, string> {{"mobile", message.PhoneNumber}, {"message", $"{message.Content}{_config.SignName}"}};

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendAsync(auth, content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        public async Task<LuosimaoSendResult> SendCodeAsync(LuosimaoSmsCode code) {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_luosimaoAccount.Password)) throw new ArgumentNullException(nameof(_luosimaoAccount.Password));
            if (string.IsNullOrWhiteSpace(_config.SignName)) throw new ArgumentNullException(nameof(_config.SignName));

            code.CheckParameters();

            var auth = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"api:key-{_luosimaoAccount.Password}"));

            var bizParams = new Dictionary<string, string> {{"mobile", code.PhoneNumber}, {"message", $"{code.Content}{_config.SignName}"}};

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendAsync(auth, content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        private static LuosimaoSendResult ReturnAsDefautlResponse()
            => new LuosimaoSendResult {
                Error = 500,
                Msg = "解析错误，返回默认结果"
            };
    }
}