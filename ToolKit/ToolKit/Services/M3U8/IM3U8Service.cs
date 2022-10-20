using ToolKit.Services.M3U8.Models;

namespace ToolKit.Services.M3U8
{
    public interface IM3U8Service
    {
        public static string Url = "https://91porny.com/";
        public static string DayTop = "video/category/hot-list";
        public static string Month = "";
        public static string MonthLast = "";

        /// <summary>
        /// 获取链接源代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns>源代码</returns>
        Task<List<M3U8DTO>> DowM3U8();
    }
}
