using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            //Process exe = new Process();
            //exe.StartInfo.FileName = "F:/DawdlerToolKit/ToolKit/ToolKit/N_m3u8DL-CLI_v3.0.2.exe";
            //exe.StartInfo.Arguments = "https://cdn3.jiuse.cloud/hls/716498/index.m3u8?t=1666445704&m=l6N9qfZQCoXcq_K8-bYR_Q --saveName 90后极品空姐，操到高潮浪叫 --workDir F:/pro";
            //exe.Start();
            //exe.WaitForExit();
            var list = await m3u8.DowM3U8(IM3U8Service.Url + IM3U8Service.Month);
            //await m3u8.CallExe(list);
            return Ok(new BaseResult("", 200, "请求成功"));
        }
    }
}
