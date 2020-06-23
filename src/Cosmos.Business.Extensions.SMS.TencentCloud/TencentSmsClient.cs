using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.TencentCloud.Configuration;
using Cosmos.Business.Extensions.SMS.TencentCloud.Core;
using Cosmos.Business.Extensions.SMS.TencentCloud.Models;
using Cosmos.Business.Extensions.SMS.TencentCloud.Models.Results;
using qcloudsms_csharp;

namespace Cosmos.Business.Extensions.SMS.TencentCloud
{
    /// <summary>
    /// Tencent Cloud SMS / QCloud SMS
    /// </summary>
    /// <remarks>Need to refactor</remarks>
    public class TencentSmsClient : SmsClientBase
    {
        private readonly TencentSmsConfig _config;
        private readonly TencentAccount _tencentAccount;
        private readonly TencentSmsSenderProxy _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public TencentSmsClient(TencentSmsConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _tencentAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));

            _proxy = new TencentSmsSenderProxy(_tencentAccount.AppId, _tencentAccount.AppKey);

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<TencentSmsSendResponseData> SendAsync(TencentSmsSendMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (_tencentAccount.AppId <= 0) throw new ArgumentOutOfRangeException(nameof(_tencentAccount.AppId));
            if (string.IsNullOrWhiteSpace(_tencentAccount.AppKey)) throw new ArgumentNullException(nameof(_tencentAccount.AppKey));

            message.CheckParameters();

            var single = message.PhoneNumbers.Count == 1;

            if (single)
            {
                var sender = _proxy.GetSingleSender();

                var response = sender.send(0, message.NationCode, message.PhoneNumbers[0], message.Content, "", "");

                return Convert(response);
            }
            else
            {
                var sender = _proxy.GetMultiSender();

                var response = sender.send(0, message.NationCode, message.PhoneNumbers, message.Content, "", "");

                return Convert(response);
            }
        }

        public  Task<TencentSmsSendResponseData> SendCodeAsync(TencentSmsSendCode code) => SendAsync(code);

        private static TencentSmsSendResponseData Convert(SmsSingleSenderResult response)
        {
            return new TencentSmsSendResponseData
            {
                Result = response.result,
                ErrMsg = response.errMsg,
                Ext = response.ext,
                Fee = response.fee,
                Sid = response.sid
            };
        }

        private static TencentSmsSendResponseData Convert(SmsMultiSenderResult response)
        {
            var ret = new TencentSmsSendResponseData
            {
                Result = response.result,
                ErrMsg = response.errMsg,
                Ext = response.ext,
                Detail = new List<TencentSmsSendResult>()
            };


            foreach (var item in response.details)
            {
                ret.Detail.Add(new TencentSmsSendResult
                {
                    Result = item.result,
                    ErrMsg = item.errmsg,
                    Sid = item.sid,
                    Fee = item.fee,
                    Mobile = item.mobile,
                    NationCode = item.nationcode
                });
            }

            return ret;
        }
        
        public override void CheckMyself() { }
    }
}