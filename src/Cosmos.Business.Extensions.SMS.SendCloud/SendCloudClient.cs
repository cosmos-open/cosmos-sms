using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Abstractions;
using Cosmos.Business.Extensions.SMS.SendCloud.Configuration;
using Cosmos.Business.Extensions.SMS.SendCloud.Core;
using Cosmos.Business.Extensions.SMS.SendCloud.Core.Helpers;
using Cosmos.Business.Extensions.SMS.SendCloud.Models;
using Cosmos.Business.Extensions.SMS.SendCloud.Models.Results;
using WebApiClient;

namespace Cosmos.Business.Extensions.SMS.SendCloud {
    public class SendCloudClient : ISMSClient<SendCloudSmsMessage, SendCloudSmsCode, ResponseData<SmsCalledResult>> {
        private readonly SendCloudConfig _config;
        private readonly SendCloudAccount _sendCloudAccount;
        private readonly ISendCloudApi _proxy;
        private readonly Action<Exception> _exceptionHandler;

        public SendCloudClient(SendCloudConfig config, Action<Exception> exceptionHandler = null) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _sendCloudAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));
            _proxy = HttpApiClient.Create<ISendCloudApi>();

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public async Task<ResponseData<TimeStampResult>> GetTimeStampAsync() {
            return await _proxy.GetTimeStampAsync();
        }

        public async Task<ResponseData<SmsCalledResult>> SendAsync(SendCloudSmsMessage message) {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_sendCloudAccount.SmsUser)) throw new ArgumentNullException(nameof(_sendCloudAccount.SmsUser));
            if (string.IsNullOrWhiteSpace(_sendCloudAccount.SmsKey)) throw new ArgumentNullException(nameof(_sendCloudAccount.SmsKey));

            message.CheckParameters();

            var timestamp = await GetTimeStampAsync();

            var bizParams = new SortedDictionary<string, string> {
                {"smsUser", _sendCloudAccount.SmsUser},
                {"msgType", message.MsgType.ToString()},
                {"phone", message.GetPhoneString()},
                {"templateId", message.TemplateId.ToString()},
                {"timestamp", timestamp.Info.Timestamp.ToString()}
            };

            if (message.Vars?.Count > 0)
                bizParams.Add("vars", message.GetVarsString());

            var signature = SignatureHelper.GetApiSignature(bizParams, _sendCloudAccount.SmsKey);
            bizParams.Add("signature", signature);

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendMessageAsync(content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        public async Task<ResponseData<SmsCalledResult>> SendCodeAsync(SendCloudSmsCode code) {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(_sendCloudAccount.SmsUser)) throw new ArgumentNullException(nameof(_sendCloudAccount.SmsUser));
            if (string.IsNullOrWhiteSpace(_sendCloudAccount.SmsKey)) throw new ArgumentNullException(nameof(_sendCloudAccount.SmsKey));

            code.CheckParameters();

            var timestamp = await GetTimeStampAsync();

            var bizParams = new SortedDictionary<string, string> {
                {"smsUser", _sendCloudAccount.SmsUser},
                {"code", code.Code},
                {"phone", code.Phone},
                {"timestamp", timestamp.Info.Timestamp.ToString()}
            };

            if (code.LabelId != null) {
                bizParams.Add("labelId", code.LabelId.Value.ToString());
            }

            var signature = SignatureHelper.GetApiSignature(bizParams, _sendCloudAccount.SmsKey);
            bizParams.Add("signature", signature);

            var content = new FormUrlEncodedContent(bizParams);

            return await _proxy.SendCodeAsync(content)
                .Retry(_config.RetryTimes)
                .Handle().WhenCatch<Exception>(e => {
                    _exceptionHandler?.Invoke(e);
                    return ReturnAsDefautlResponse();
                });
        }

        private static ResponseData<SmsCalledResult> ReturnAsDefautlResponse()
            => new ResponseData<SmsCalledResult> {
                StatusCode = 500,
                Message = "解析错误，返回默认结果"
            };
    }
}