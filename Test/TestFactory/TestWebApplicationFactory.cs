using System.Linq;
using DotnetUnitTest;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
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
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        public HttpClient Create(Action<MockHttpMessageHandler> mockHttp = null)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
               {
                   var descriptior = services.SingleOrDefault(d => d.ServiceType == typeof(HttpClient));
                   if (descriptior != null)
                   {
                       services.Remove(descriptior);
                   }

                   services.AddTransient<HttpClient>(svr =>
                   {
                       var mock = new MockHttpMessageHandler();
                       mockHttp?.Invoke(mock);

                       return new HttpClient(mock);
                   });
               });
            }).CreateClient();
        }
    }
}
