using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.Yunpian.Configuration;
using Cosmos.Business.Extensions.SMS.Yunpian.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;
using Yunpian.Sdk;

namespace Cosmos.Business.Extensions.SMS.Yunpian.Tests {
    public class YunpianSmsTests {
        private readonly YunpianConfig _config;
        private readonly YunpianSmsClient _client;

        private string _messageIfError { get; set; }

        public YunpianSmsTests() {
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

            _config = configuration.GetSection("SMS:Yunpian").Get<YunpianConfig>();
            _client = new YunpianSmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.Account.ApiKey);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new YunpianSmsCode {
                PhoneNumber = "",
                Content = "【云片网】您的验证码是1234"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.IsSucc(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
        }

        [Fact]
        public async void SendMessageTest() {
            var message = new YunpianSmsMessage {
                PhoneNumbers = new List<string> {""},
                Content = "【云片网】您的验证码是1234"
            };

            var response = await _client.SendAsync(message);

            Assert.NotNull(response);
            Assert.True(response.IsSucc(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
        }

    }
}