using System.IO;
using Cosmos.Business.Extensions.SMS.RongCloud.Configuration;
using Cosmos.Business.Extensions.SMS.RongCloud.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.RongCloud.Tests {
    public class RongCloudSmsTests {
        private readonly RongCloudConfig _config;
        private readonly RongCloudClient _client;

        public RongCloudSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:RongCloud").Get<RongCloudConfig>();
            _client = new RongCloudClient(_config);
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
        }

        [Fact]
        public async void SendSmsTest() {
            var message = new RongCloudSmsMessage {
                Mobile = "",
                TemplateId = "123",
            };

            message.Vars.Add(1, "311851");

            var response = await _client.SendCodeAsync(message);

            Assert.True(response.Code == 200, JsonConvert.SerializeObject(response));
        }
    }
}