using System;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.BaiduYun.Configuration;
using Cosmos.Business.Extensions.SMS.BaiduYun.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.BaiduYun.Tests {
    public class BaiduYunTests {
        private readonly BaiduYunConfig _config;
        private readonly BaiduYunSmsClient _client;

        private string _messageIfError { get; set; }

        public BaiduYunTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:BaiduYun").Get<BaiduYunConfig>();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _client = new BaiduYunSmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.Account.AccessKeyId);
            Assert.NotEmpty(_config.Account.SecretAccessKey);
            Assert.NotEmpty(_config.InvokeId);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new BaiduYunCode {
                PhoneNumber = "",
                TemplateCode = "smsTpl:6nHdNumZ4ZtGaKO",
                Code = "311920",
                CodeKey = "code"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.Code == 0, $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
            //Assert.True(string.IsNullOrWhiteSpace(response), response);
        }

        [Fact]
        public async void SendTest() {
            var message = new BaiduYunMessage() {
                PhoneNumber = "",
                TemplateCode = "smsTpl:6nHdNumZ4ZtGaKO",
            };

            message.Vars.Add("code", "31990");

            var response = await _client.SendAsync(message);

            Assert.NotNull(response);
            Assert.True(response.Code == 0, $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
            //Assert.True(string.IsNullOrWhiteSpace(response), response);
        }


    }
}