using System.Collections.Generic;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.Weimi.Configuration;
using Cosmos.Business.Extensions.SMS.Weimi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.Weimi.Tests {
    public class WeimiSmsTests {
        private readonly WeimiSmsConfig _config;
        private readonly WeimiSmsClient _client;

        private string _messageIfError { get; set; }

        public WeimiSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:Weimi").Get<WeimiSmsConfig>();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _client = new WeimiSmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.Account.Uid);
            Assert.NotEmpty(_config.Account.Pas);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new WeimiSmsCode {
                PhoneNumbers = new List<string> {""},
                TemplateId = ""
            };

            code.TemplateParams.Add(1, "311920");

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.IsSuccess(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
        }

        public async void SendMessageTest() {
            var message = new WeimiSmsMessage {
                PhoneNumbers = new List<string> {""},
                Content = "无名英雄"
            };

            var response = await _client.SendAsync(message);

            Assert.NotNull(response);
            Assert.True(response.IsSuccess(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
        }
    }
}