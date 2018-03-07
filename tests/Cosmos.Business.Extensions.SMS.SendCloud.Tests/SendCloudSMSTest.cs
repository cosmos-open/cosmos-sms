using System;
using System.Collections.Generic;
using System.IO;
using Cosmos.Business.Extensions.SMS.SendCloud.Configuration;
using Cosmos.Business.Extensions.SMS.SendCloud.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.SendCloud.Tests {
    public class SendCloudSmsTest {
        private readonly SendCloudConfig _config;
        private readonly SendCloudClient _client;

        public SendCloudSmsTest() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:SendCloud").Get<SendCloudConfig>();
            _client = new SendCloudClient(_config);
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
        }

        [Fact]
        public async void GetTimeStamp() {
            var timestamp = await _client.GetTimeStampAsync();
            Assert.NotNull(timestamp);
            Assert.NotNull(timestamp.Info);
            Assert.True(timestamp.Info.Timestamp > 0);
        }

        /// <summary>
        /// this ut is same to https://github.com/LonghronShen/SendCloudSDK/blob/netstandard2.0/test/SendCloudSDK.Test/SendCloudTests.cs#L53
        /// </summary>
        [Fact]
        public async void SendSmsTest() {
            var message = new SendCloudSmsMessage {
                Phone = new List<string> {""},
                TemplateId = 12570,
                Vars = new Dictionary<string, string> {
                    {"cluster", "021-上海第一人民医院"},
                    {"service", "DB"},
                    {"time", DateTime.Now.ToString()}
                }
            };

            var response = await _client.SendAsync(message);

            Assert.True(response.StatusCode == 200, JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new SendCloudSmsCode {
                Phone = "",
                Code = "311920"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.True(response.StatusCode == 200, JsonConvert.SerializeObject(response));
        }
    }
}