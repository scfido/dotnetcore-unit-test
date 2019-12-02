using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class AppSettingsTest: IClassFixture<TestWebApplicationFactory<Startup>>
    {
        IConfiguration config;
        public AppSettingsTest(TestWebApplicationFactory<Startup> facotry)
        {
            // var server = host.Create();
            // config = server.Features.Get<IConfiguration>();
            // config = (IConfiguration)server.Host.Services.GetService(typeof(IConfiguration));
        }

        [Fact]
        public void LoggingLevelConfigTest()
        {
           // Assert.Equal("Warning", config.GetValue<string>("Logging:LogLevel:Default"));
        }
    }
}
