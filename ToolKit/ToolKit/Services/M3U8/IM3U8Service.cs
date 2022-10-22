using ToolKit.Services.M3U8.Models;

namespace ToolKit.Services.M3U8
{
    public interface IM3U8Service
    {
        public static string Url = "https://91porny.com/";
        public static string DayTop = "video/category/hot-list";
        public static string Month = "video/category/top-list";
        public static string MonthLast = "video/category/top-last";

        /// <summary>
        /// 获取链接源代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns>源代码</returns>
        Task<List<M3U8DTO>> DowM3U8(string url);

        /// <summary>
        /// 调用m3u8下载器
        /// </summary>
        /// <param name="m3u8"></param>
        /// <returns></returns>
        Task CallExe(List<M3U8DTO> m3u8);
    }
}
