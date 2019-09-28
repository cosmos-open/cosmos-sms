using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.ChuangLan.Configuration;
using Cosmos.Business.Extensions.SMS.ChuangLan.Core;
using Cosmos.Business.Extensions.SMS.ChuangLan.Core.Helpers;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.ChuangLan
{
    public class ChuanglanClient : SmsClientBase
    {
        private readonly ChuanglanConfig _config;
        private readonly ChuanglanAccount _chuanglanAccount;
        private readonly IChuanglanApi _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public ChuanglanClient(ChuanglanConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _chuanglanAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));
            _proxy = WebApiClientCreator.Create(config);

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<ResponseData> SendAsync(ChuanglanSmsMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            _chuanglanAccount.CheckParameters();

            message.CheckParameters();

            var bizParams = new SortedDictionary<string, string>()
            {
                {"account", _chuanglanAccount.SmsUser},
                {"password", _chuanglanAccount.SmsKey},
                {"msg", message.Content},
                {"phone", message.GetPhoneString()},
            };

            if (message.SendTime.HasValue)
            {
                bizParams.Add("sendtime", message.SendTime.Value.ToString("yyyyMMddHHmm"));
            }

            if (message.Report.HasValue && message.Report.Value)
            {
                bizParams.Add("report", "true");
            }

            if (!string.IsNullOrWhiteSpace(message.Extend))
            {
                bizParams.Add("extend", message.Extend);
            }

            if (!string.IsNullOrWhiteSpace(message.Uid))
            {
                bizParams.Add("uid", message.Uid);
            }

            return await _proxy.SendMessageAsync(bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        public async Task<VariableResponseData> SendVariableAsync(ChuanglanSmsVariableMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            _chuanglanAccount.CheckParameters();

            message.CheckParameters();

            var bizParams = new SortedDictionary<string, string>()
            {
                {"account", _chuanglanAccount.SmsUser},
                {"password", _chuanglanAccount.SmsKey},
                {"msg", message.Content},
                {"params", message.GetParamsString()},
            };

            if (message.SendTime.HasValue)
            {
                bizParams.Add("sendtime", message.SendTime.Value.ToString("yyyyMMddHHmm"));
            }

            if (message.Report.HasValue && message.Report.Value)
            {
                bizParams.Add("report", "true");
            }

            if (!string.IsNullOrWhiteSpace(message.Extend))
            {
                bizParams.Add("extend", message.Extend);
            }

            if (!string.IsNullOrWhiteSpace(message.Uid))
            {
                bizParams.Add("uid", message.Uid);
            }

            return await _proxy.SendVariableMessageAsync(bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsVariableDefautlResponse();
                });
        }

        public async Task<ResponseData> SendCodeAsync(ChuanglanSmsCode code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            _chuanglanAccount.CheckParameters();
            code.CheckParameters();

            var bizParams = new SortedDictionary<string, string>()
            {
                {"account", _chuanglanAccount.SmsUser},
                {"password", _chuanglanAccount.SmsKey},
                {"msg", code.Msg},
                {"phone", code.Phone},
            };

            if (code.SendTime.HasValue)
            {
                bizParams.Add("sendtime", code.SendTime.Value.ToString("yyyyMMddHHmm"));
            }

            if (code.Report.HasValue && code.Report.Value)
            {
                bizParams.Add("report", "true");
            }

            if (!string.IsNullOrWhiteSpace(code.Extend))
            {
                bizParams.Add("extend", code.Extend);
            }

            if (!string.IsNullOrWhiteSpace(code.Uid))
            {
                bizParams.Add("uid", code.Uid);
            }

            return await _proxy.SendCodeAsync(bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        private static ResponseData ReturnAsDefautlResponse()
            => new ResponseData
            {
                Code = "500",
                ErrorMsg = "解析错误，返回默认结果"
            };

        private static VariableResponseData ReturnAsVariableDefautlResponse()
            => new VariableResponseData
            {
                Code = "500",
                ErrorMsg = "解析错误，返回默认结果"
            };
        
        public override void CheckMyself() { }
    }
}