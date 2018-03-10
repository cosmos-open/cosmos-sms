using System;
using System.Collections.Generic;
using System.IO;
using Cosmos.Business.Extensions.SMS.TencentCloud.Configuration;
using Cosmos.Business.Extensions.SMS.TencentCloud.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.TencentCloud.Tests {
    public class TencentSmsTests {
        private readonly TencentSmsConfig _config;
        private readonly TencentSmsClient _client;

        public TencentSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:TencentCloud").Get<TencentSmsConfig>();
            _client = new TencentSmsClient(_config);
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
        }

        [Fact]
        public async void SendSmsTest() {
            var message = new TencentSmsSendMessage {
                PhoneNumbers = new List<string> {"13818167501"},
                Content = "验证码为12345"
            };

            var response = await _client.SendAsync(message);

            Assert.True(response.Result == 0, JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new TencentSmsSendCode {
                PhoneNumbers = new List<string> {"13818167501"},
                Content = "验证码为12345"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.True(response.Result == 0, JsonConvert.SerializeObject(response));
        }
    }
}