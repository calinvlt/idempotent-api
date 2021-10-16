using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Idempotent.Core.Tests
{
    public class IdempotentMiddlewareTests
    {
        private async Task<IHost> CreateHttpClient()
        {
            return await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.AddControllers();
                            services.AddDistributedMemoryCache();
                        })
                        .Configure(app =>
                        {
                            
                            app.UseMiddleware<IdempotentMiddleware>();
                            app.UseRouting();
                            app.UseEndpoints(endpoints =>
                            {
                                endpoints.MapControllers();
                            });
                        });
                })
                .StartAsync();
        }
        
        [Fact]
        public async Task Get_Test_Controller_Returns_OK()
        {
            using var host = await CreateHttpClient();
            var client = host.GetTestClient();
            var response = await client.GetAsync("/sample");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}