using System.Collections.Generic;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.JdCloud.Configuration;
using Cosmos.Business.Extensions.SMS.JdCloud.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.JdCloud.Tests {
    public class JdCloudSmsTests {
        private readonly JdCloudSmsConfig  _config;
        private readonly JdCloudSmsClient _client;

        private string _messageIfError { get; set; }

        public JdCloudSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:JdCloud").Get<JdCloudSmsConfig>();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _client = new JdCloudSmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.RegionId);
            Assert.NotEmpty(_config.SignId);
            Assert.NotEmpty(_config.TemplateId);
            Assert.NotEmpty(_config.Account.AccessKey);
            Assert.NotEmpty(_config.Account.SecretKey);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new JdCloudSmsCode {
                PhoneNumberList = new List<string> {""},
                Params = new List<string>{"311920"}
            };

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.IsSuccess(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
            //Assert.True(string.IsNullOrWhiteSpace(response), response);
        }
    }
}