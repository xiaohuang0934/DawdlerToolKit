using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolKit.Commons.BaseResults;
using ToolKit.Configs.AutoFacs;
using ToolKit.Services.M3U8;
using ToolKit.Services.Test;

namespace ToolKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class M3U8Controller : ControllerBase
    {
        [Autowired]
        private IM3U8Service m3u8 { get; set; }

        [HttpGet]
        public async Task<IActionResult> QueryList()
        {
            await m3u8.DowM3U8();
            return Ok(new BaseResult("", 200, "请求成功"));
        }
    }
}
