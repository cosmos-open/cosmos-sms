using System.Collections.Generic;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Configuration;
using Cosmos.Business.Extensions.SMS.HuaweiCloud.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.HuaweiCloud.Tests {
    public class HuaweiCloudSmsTests {
        private readonly HuaweiCloudSmsConfig  _config;
        private readonly HuaweiCloudSmsClient _client;

        private string _messageIfError { get; set; }

        public HuaweiCloudSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:HuaweiCloud").Get<HuaweiCloudSmsConfig>();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _client = new HuaweiCloudSmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.RegionName);
            Assert.NotEmpty(_config.Sender);
            Assert.NotEmpty(_config.TemplateId);
            Assert.NotEmpty(_config.Account.AppKey);
            Assert.NotEmpty(_config.Account.AppSecret);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new HuaweiCloudSmsCode {
                PhoneNumberList = new List<PhoneNumberEntity> {new PhoneNumberEntity("86","13800100001")},
                Params = new List<string>{"311920"}
            };

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.IsSuccess(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
            //Assert.True(string.IsNullOrWhiteSpace(response), response);
        }
    }
}