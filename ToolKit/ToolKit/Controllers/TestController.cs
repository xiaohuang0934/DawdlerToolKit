using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolKit.Configs.AutoFacs;
using ToolKit.Services.Test;

namespace ToolKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Autowired]
        private ITestService Test { get; set; }

        [HttpGet]
        public async Task<IActionResult> QueryString()
        {
            return Ok(await Test.QueryString());
        }
    }
}
