using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using parpy.dotnetcore.prctc.Web.ms;

namespace parpy.dotnetcore.prctc.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IValuesService _valuesService;
        public ValuesController(IValuesService valuesService)
        {
            _valuesService = valuesService;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var str = await _valuesService.GetValues();
            return Ok(str);
        }
        
    }
}
