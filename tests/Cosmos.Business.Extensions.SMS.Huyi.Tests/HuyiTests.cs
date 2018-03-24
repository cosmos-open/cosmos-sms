using System;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.Huyi.Configuration;
using Cosmos.Business.Extensions.SMS.Huyi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.Huyi.Tests {
    public class HuyiTests {
        private readonly HuyiConfig _config;
        private readonly HuyiClient _client;

        private string _messageIfError { get; set; }

        public HuyiTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:Huyi").Get<HuyiConfig>();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _client = new HuyiClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.Account.AppId);
            Assert.NotEmpty(_config.Account.ApiKey);
        }

        [Fact]
        public async void SendCodeTest() {
            var message = new HuyiMessage {
                PhoneNumber = "",
                Message = "您的验证码是：12345。请不要把验证码泄露给其他人。"
            };

            var response = await _client.SendAsync(message);

            Assert.NotNull(response);
            Assert.True(response.Code == 2, $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
            //Assert.True(string.IsNullOrWhiteSpace(response), response);
        }
    }
}