using System.IO;
using Cosmos.Business.Extensions.SMS.Luosimao.Configuration;
using Cosmos.Business.Extensions.SMS.Luosimao.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.Luosimao.Tests {
    public class LuosimaoSmsTests {
        private readonly LuosimaoConfig _config;
        private readonly LuosimaoClient _client;

        public LuosimaoSmsTests() {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _config = configuration.GetSection("SMS:Luosimao").Get<LuosimaoConfig>();
            _client = new LuosimaoClient(_config);
        }

        [Fact]
        public void ConfigChecking() {
            Assert.NotNull(_config);
            Assert.NotNull(_config.Account);
        }


        [Fact]
        public async void SendSmsTest() {
            var message = new LuosimaoSmsMessage {
                PhoneNumber = "",
                Content = "模板是12345"
            };

            var response = await _client.SendAsync(message);

            Assert.True(response.Error == 0, JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async void SendCodeTest() {
            var code = new LuosimaoSmsCode {
                PhoneNumber = "",
                Content = "模板是12345"
            };

            var response = await _client.SendCodeAsync(code);

            Assert.True(response.Error == 0, JsonConvert.SerializeObject(response));
        }
    }
}