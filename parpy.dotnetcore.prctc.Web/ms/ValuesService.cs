using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace parpy.dotnetcore.prctc.Web.ms
{
    public class ValuesService : IValuesService
    {
        private static readonly string _base_values_service_url = "http://values-service/";
        private readonly IHttpClientFactory _httpClientFactory;
        public ValuesService(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }
        public Task<string> GetValues()
        {
            var url = $"{_base_values_service_url}api/values";
            return _httpClientFactory.CreateClient("msclient").GetStringAsync(url);
        }
    }
}
