using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix;
using Steeltoe.Common.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace parpy.dotnetcore.prctc.Web.ms
{
    public class EurekaService : HystrixCommand<string>, IEurekaService
    {
        private readonly string _base_eureka_url = "http://eureka-service/";

        private IHttpClientFactory _httpFactory;

        public EurekaService(IHystrixCommandOptions options, IHttpClientFactory httpClient) 
            : base(options)
        {
            _httpFactory = httpClient;
            IsFallbackUserDefined = true;
        }

        public Task<string> GetServices()
        {
            var url = $"{_base_eureka_url}api/values";
            var task = _httpFactory.CreateClient("msclient").GetStringAsync(url);
            return task;

        }

        public async Task<string> GetServicesWithHystrix()
        {
            var result = await ExecuteAsync();
            return result;
        }

        protected override async Task<string> RunAsync()
        {
            return await GetServices();
        }

        protected override async Task<string> RunFallbackAsync()
        {
            return await Task.FromResult("This is a error（服务断开，稍后重试）!");
        }

    }
}
