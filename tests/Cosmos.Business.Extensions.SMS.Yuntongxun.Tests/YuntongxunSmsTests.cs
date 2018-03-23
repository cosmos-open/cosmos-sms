using System.Collections.Generic;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Configuration;
using Cosmos.Business.Extensions.SMS.Yuntongxun.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.Yuntongxun.Tests {
    public class YuntongxunSmsTests {
        private readonly YuntongxunSmsConfig _config;
        private readonly YuntongxunSmsClient _client;

        private string _messageIfError { get; set; }

        public YuntongxunSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _config = configuration.GetSection("SMS:Yuntongxun").Get<YuntongxunSmsConfig>();
            _client = new YuntongxunSmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.Account.AccountSid);
            Assert.NotEmpty(_config.Account.AccountToken);
            Assert.NotEmpty(_config.Account.AppId);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new YuntongxunCode {
                PhoneLists = new List<string> {""},
                TemplateId = "27026",
                Code = "12345"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.statusCode == "0", $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
        }
    }
}