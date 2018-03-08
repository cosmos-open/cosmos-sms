using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cosmos.Business.Extensions.SMS.Aliyun.Configuration;
using Cosmos.Business.Extensions.SMS.Aliyun.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.Aliyun.Tests {
    public class AliyunDysmsTests {
        private readonly AliyunDysmsConfig _config;
        private readonly AliyunDysmsClient _client;

        private string _messageIfError { get; set; }

        public AliyunDysmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:Aliyun").Get<AliyunDysmsConfig>();

            SMS.Exceptions.ExceptionHandleResolver.SetHandler(e => {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.Source);
                sb.AppendLine(e.StackTrace);
                _messageIfError += sb.ToString();
            });

            _client = new AliyunDysmsClient(_config, SMS.Exceptions.ExceptionHandleResolver.ResolveHandler());
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
            Assert.NotEmpty(_config.SignName);
            Assert.NotEmpty(_config.Account.AccessKeyId);
            Assert.NotEmpty(_config.Account.AccessKeySecret);
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new AliyunDysmsCode {
                TemplateCode = "",
                Phone = new List<string> {""},
                Code = "311920"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.NotNull(response);
            Assert.True(response.IsSuccess(), $"{JsonConvert.SerializeObject(response)},{_messageIfError}");
            //Assert.True(string.IsNullOrWhiteSpace(response), response);
        }
    }
}