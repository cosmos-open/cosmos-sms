using System;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.TencentCloud.Configuration;
using Cosmos.Business.Extensions.SMS.TencentCloud.Core;

namespace Cosmos.Business.Extensions.SMS.TencentCloud {
    public class TencentSmsClient {
        private readonly TencentSmsConfig _config;
        private readonly TencentAccount _tencentAccount;
        private readonly TencentSmsSenderProxy _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public TencentSmsClient(TencentSmsConfig config, Action<Exception> exceptionHandler) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _tencentAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));

            _proxy = new TencentSmsSenderProxy(_tencentAccount.AppId, _tencentAccount.AppKey);

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }
    }
}