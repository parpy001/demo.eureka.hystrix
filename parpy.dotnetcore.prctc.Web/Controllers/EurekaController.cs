using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using parpy.dotnetcore.prctc.Web.Models;
using parpy.dotnetcore.prctc.Web.ms;
using parpy.dotnetcore.prctc.Web.Utils;

namespace parpy.dotnetcore.prctc.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EurekaController : ControllerBase
    {
        private IEurekaService _eurekaClientService;
        private ILogger<EurekaController> _logger;
        public EurekaController(IEurekaService eurekaClientService, ILogger<EurekaController> logger)
        {
            _eurekaClientService = eurekaClientService;
            _logger = logger;
        }

        // GET: api/services
        [HttpGet("services")]
        public async Task<IActionResult> GetServices()
        {
            _logger?.LogInformation("api/fortunes/services");
            return Ok(await _eurekaClientService.GetServices());
        }

        [HttpGet("serviceshystrix")]
        public async Task<IActionResult> GetServicesWithHystrix()
        {
            _logger?.LogInformation("api/fortunes/serviceshystrix");
            return Ok(await _eurekaClientService.GetServicesWithHystrix());
        }

        [HttpGet("objjson")]
        public Task<object> GetObjectJson()
        {
            var obj = new ObjectModel();
            
            return Task.FromResult<object>(Ok(JsonUtil.SerializeObject(obj)));

        }
    }
}