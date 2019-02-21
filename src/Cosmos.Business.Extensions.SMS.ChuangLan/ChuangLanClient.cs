using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.ChuangLan.Configuration;
using Cosmos.Business.Extensions.SMS.ChuangLan.Core;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models.Results;
using Cosmos.Business.Extensions.SMS.Exceptions;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.ChuangLan
{
    public class ChuangLanClient
    {
        private readonly ChuangLanConfig _config;
        private readonly ChuangLanAccount _chuangLanCodeAccount;
        private readonly ChuangLanAccount _chuangLanMarketingAccount;
        private readonly IChuangLanApi _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public ChuangLanClient(ChuangLanConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _chuangLanCodeAccount = config.CodeAccount ?? throw new ArgumentNullException(nameof(config.CodeAccount));
            if (config.UseMarketingSms)
            {
                _chuangLanMarketingAccount = config.MarketingAccount ??
                                             throw new ArgumentNullException(nameof(config.MarketingAccount));
            }
            _proxy = HttpApiClient.Create<IChuangLanApi>();

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<ResponseData> SendAsync(ChuangLanSmsMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (_config.UseMarketingSms)
            {
                _chuangLanMarketingAccount.CheckParameters();
            }
            else
            {
                _chuangLanCodeAccount.CheckParameters();
            }

            message.CheckParameters();

            var bizParams = new SortedDictionary<string, string>()
            {
                {
                    "account",
                    _config.UseMarketingSms ? _chuangLanMarketingAccount.SmsUser : _chuangLanCodeAccount.SmsUser
                },
                {
                    "password",
                    _config.UseMarketingSms ? _chuangLanMarketingAccount.SmsKey : _chuangLanCodeAccount.SmsKey
                },
                {
                    "msg", message.Content
                },
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

            _proxy.ApiConfig.HttpHost = new Uri(_config.UseMarketingSms ? _chuangLanMarketingAccount.ApiUrl : _chuangLanCodeAccount.ApiUrl);
            return await _proxy.SendMessageAsync(bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        public async Task<VariableResponseData> SendVariableAsync(ChuangLanSmsVariableMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (_config.UseMarketingSms)
            {
                _chuangLanMarketingAccount.CheckParameters();
            }
            else
            {
                _chuangLanCodeAccount.CheckParameters();
            }

            message.CheckParameters();

            var bizParams = new SortedDictionary<string, string>()
            {
                {
                    "account",
                    _config.UseMarketingSms ? _chuangLanMarketingAccount.SmsUser : _chuangLanCodeAccount.SmsUser
                },
                {
                    "password",
                    _config.UseMarketingSms ? _chuangLanMarketingAccount.SmsKey : _chuangLanCodeAccount.SmsKey
                },
                {
                    "msg", message.Content
                },
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

            _proxy.ApiConfig.HttpHost = new Uri(_config.UseMarketingSms ? _chuangLanMarketingAccount.ApiUrl : _chuangLanCodeAccount.ApiUrl);
            return await _proxy.SendVariableMessageAsync(bizParams)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e =>
                {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsVariableDefautlResponse();
                });
        }

        public async Task<ResponseData> SendCodeAsync(ChuangLanSmsCode code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            _chuangLanCodeAccount.CheckParameters();
            code.CheckParameters();

            var bizParams = new SortedDictionary<string, string>()
            {
                {"account", _chuangLanCodeAccount.SmsUser},
                {"password", _chuangLanCodeAccount.SmsKey},
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

            _proxy.ApiConfig.HttpHost = new Uri(_chuangLanCodeAccount.ApiUrl);
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
    }
}
