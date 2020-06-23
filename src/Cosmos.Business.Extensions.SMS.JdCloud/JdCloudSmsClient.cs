using System;
using System.Threading.Tasks;
using Cosmos.Business.Extensions.SMS.Client;
using Cosmos.Business.Extensions.SMS.Exceptions;
using Cosmos.Business.Extensions.SMS.JdCloud.Configuration;
using Cosmos.Business.Extensions.SMS.JdCloud.Core;
using Cosmos.Business.Extensions.SMS.JdCloud.Models;
using Cosmos.Business.Extensions.SMS.JdCloud.Models.Results;
using JDCloudSDK.Core.Auth;
using JDCloudSDK.Core.Http;
using JDCloudSDK.Sms.Apis;
using JDCloudSDK.Sms.Client;

namespace Cosmos.Business.Extensions.SMS.JdCloud
{
    public class JdCloudSmsClient : SmsClientBase
    {
        private readonly JdCloudSmsConfig _config;
        private readonly JdCloudAccount _jdcloudAccount;
        private readonly SmsClient _client;
        private readonly Action<Exception> _exceptionHandler;

        public JdCloudSmsClient(JdCloudSmsConfig config, Action<Exception> exceptionHandler = null)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _jdcloudAccount = config.Account ?? throw new ArgumentNullException(nameof(config.Account));

            CredentialsProvider credentialsProvider = new StaticCredentialsProvider(_jdcloudAccount.AccessKey, _jdcloudAccount.SecretKey);

            _client = new SmsClient.DefaultBuilder()
                .CredentialsProvider(credentialsProvider)
                .HttpRequestConfig(new HttpRequestConfig(_config.Security ? Protocol.HTTPS : Protocol.HTTP, _config.RequestTimeout))
                .Build();

            var globalHandle = ExceptionHandleResolver.ResolveHandler();
            globalHandle += exceptionHandler;
            _exceptionHandler = globalHandle;
        }

        public Task<JdCloudSmsResult> SendAsync(JdCloudSmsMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(_jdcloudAccount.AccessKey)) throw new ArgumentNullException(nameof(_jdcloudAccount.AccessKey));
            if (string.IsNullOrWhiteSpace(_jdcloudAccount.SecretKey)) throw new ArgumentNullException(nameof(_jdcloudAccount.SecretKey));
            if (string.IsNullOrWhiteSpace(_config.SignId)) throw new ArgumentNullException(nameof(_config.SignId));
            if (string.IsNullOrWhiteSpace(_config.TemplateId)) throw new ArgumentNullException(nameof(_config.TemplateId));

            message.CheckParameters();

            var request = new BatchSendRequest
            {
                RegionId = _config.RegionId,
                TemplateId = _config.TemplateId,
                SignId = _config.SignId,
                PhoneList = message.PhoneNumberList,
                Params = message.Params
            };

            return SmsCoreSender.SendAsync(_client, request, _config);
        }

        public Task<JdCloudSmsResult> SendCodeAsync(JdCloudSmsCode code) => SendAsync(code);

        public override void CheckMyself() { }
    }
}