using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Cosmos.Business.Extensions.SMS.ChuangLan.Configuration;
using Cosmos.Business.Extensions.SMS.ChuangLan.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Cosmos.Business.Extensions.SMS.ChuangLan.Tests
{
    public class ChuangLanSmsTest
    {
        private readonly ChuangLanConfig _config;
        private readonly ChuangLanClient _client;

        public ChuangLanSmsTest()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            _config = configuration.GetSection("SMS:ChuangLan").Get<ChuangLanConfig>();
            _client=new ChuangLanClient(_config);
        }

        [Fact]
        public void ConfigChecking()
        {
            Assert.NotNull(_config);
            Assert.NotNull(_config.CodeAccount);
            Assert.NotNull(_config.MarketingAccount);
        }

        [Fact]
        public async void SendCodeTest()
        {
            var code = new ChuangLanSmsCode()
            {
                Phone = "",
                Msg = "3312047"
            };
            var response = await _client.SendCodeAsync(code);
            Assert.True(response.Code=="0",JsonConvert.SerializeObject(response));
        }
    }
}
