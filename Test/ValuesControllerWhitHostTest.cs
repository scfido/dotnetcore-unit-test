using DotnetUnitTest;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Test
{
    public class ValuesControllerWhitHostTest : IClassFixture<TestWebHost>
    {
        HttpClient client;

        public ValuesControllerWhitHostTest(TestWebHost host)
        {
            client = host.Create(mockHttp: mock =>
            {
                //拦截服务端请求baidu.com的请求
                mock.When("https://baidu.com").Respond("text/html", "百度一下就知道了");
            })
            .CreateClient();
        }

        [Fact]
        public async void BaiduTest()
        {
            var ret = await client.GetStringAsync("/api/Values/baidu");
            Assert.Equal("百度一下就知道了", ret);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetIdTest(int i)
        {
            var ret = await client.GetStringAsync($"/api/Values/{i}");
            Assert.Equal(i.ToString(), ret);
        }
    }
}
