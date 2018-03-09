using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Aliyun.Configuration;
using Cosmos.Business.Extensions.SMS.Aliyun.Core;
using Cosmos.Business.Extensions.SMS.Aliyun.Core.Extensions;
using Cosmos.Business.Extensions.SMS.Aliyun.Core.Helpers;
using Cosmos.Business.Extensions.SMS.Aliyun.Models;
using Cosmos.Business.Extensions.SMS.Aliyun.Models.Results;
using Cosmos.Business.Extensions.SMS.Exceptions;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.Aliyun {
    public class AliyunDysmsClient {
        private readonly AliyunDysmsConfig _config;
        private readonly AliyunDysmsAccount _aliyunDysmsAccount;
        private readonly IAliyunDysmsApi _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public AliyunDysmsClient(AliyunDysmsConfig config, Action<Exception> exceptionHandler = null) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _aliyunDysmsAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));

            _proxy = config.Security
                ? HttpApiClient.Create<IAliyunDysmsApi>("https://dysmsapi.aliyuncs.com")
                : HttpApiClient.Create<IAliyunDysmsApi>("http://dysmsapi.aliyuncs.com");

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<AliyunDysmsResult> SendAsync(AliyunDysmsMessage message) {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_aliyunDysmsAccount.AccessKeyId)) throw new ArgumentNullException(nameof(_aliyunDysmsAccount.AccessKeyId));
            if (string.IsNullOrWhiteSpace(_aliyunDysmsAccount.AccessKeySecret)) throw new ArgumentNullException(nameof(_aliyunDysmsAccount.AccessKeySecret));
            if (string.IsNullOrWhiteSpace(_config.SignName)) throw new ArgumentNullException(nameof(_config.SignName));

            message.FixParameters(_config);

            message.CheckParameters();

            var bizParams = new Dictionary<string, string> {
                {"RegionId", "cn-hangzhou"},
                {"Action", "SendSms"},
                {"Version", "2017-05-25"},
                {"AccessKeyId", _aliyunDysmsAccount.AccessKeyId},
                {"PhoneNumbers", message.GetPhoneString()},
                {"SignName", _config.SignName},
                {"TemplateCode", message.TemplateCode},
                {"SignatureMethod", "HMAC-SHA1"},
                {"SignatureNonce", Guid.NewGuid().ToString()},
                {"SignatureVersion", "1.0"},
                {"Timestamp", DateTime.Now.ToIso8601DateString()},
                {"Format", "JSON"}
            };

            if (!string.IsNullOrWhiteSpace(message.OutId))
                bizParams.Add("OutId", message.OutId);

            if (message.HasTemplateParams())
                bizParams.Add("TemplateParam", message.GetTemplateParamsString());

            var signature = SignatureHelper.GetApiSignature(bizParams, _aliyunDysmsAccount.AccessKeySecret);
            bizParams.Add("Signature", signature);

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendMessageAsync(content)
                .Retry(_config.RetryTimes)
                .Handle()
                .WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        public async Task<AliyunDysmsResult> SendCodeAsync(AliyunDysmsCode code) {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_aliyunDysmsAccount.AccessKeyId)) throw new ArgumentNullException(nameof(_aliyunDysmsAccount.AccessKeyId));
            if (string.IsNullOrWhiteSpace(_aliyunDysmsAccount.AccessKeySecret)) throw new ArgumentNullException(nameof(_aliyunDysmsAccount.AccessKeySecret));
            if (string.IsNullOrWhiteSpace(_config.SignName)) throw new ArgumentNullException(nameof(_config.SignName));

            code.FixParameters(_config);

            code.CheckParameters();

            var bizParams = new Dictionary<string, string> {
                {"RegionId", "cn-hangzhou"},
                {"Action", "SendSms"},
                {"Version", "2017-05-25"},
                {"AccessKeyId", _aliyunDysmsAccount.AccessKeyId},
                {"PhoneNumbers", code.GetPhoneString()},
                {"SignName", _config.SignName},
                {"TemplateCode", code.TemplateCode},
                {"TemplateParam", code.GetTemplateParamsString()},
                {"SignatureMethod", "HMAC-SHA1"},
                {"SignatureNonce", Guid.NewGuid().ToString()},
                {"SignatureVersion", "1.0"},
                {"Timestamp", DateTime.Now.ToIso8601DateString()},
                {"Format", "JSON"}
            };

            if (!string.IsNullOrWhiteSpace(code.OutId))
                bizParams.Add("OutId", code.OutId);

            var signature = SignatureHelper.GetApiSignature(bizParams, _aliyunDysmsAccount.AccessKeySecret);
            bizParams.Add("Signature", signature);

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendCodeAsync(content)
                .Retry(_config.RetryTimes)
                .Handle()
                .WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        private static AliyunDysmsResult ReturnAsDefautlResponse()
            => new AliyunDysmsResult {
                RequestId = "",
                Code = "500",
                Message = "解析错误，返回默认结果",
                BizId = ""
            };
    }
}