using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using ToolKit.Commons.BaseExceptions;
using ToolKit.Configs.AutoFacs;
using ToolKit.Services.M3U8.Models;

namespace ToolKit.Services.M3U8
{
    public class M3U8Service : IM3U8Service
    {
        [Autowired]
        private IHttpClientFactory HttpClientFactory { get; set; }

        public async Task<List<M3U8DTO>> DowM3U8()
        {
            //Regex httpRegex = new Regex(@"[a-zA-z]+://[^\s]*");
            //if (!httpRegex.IsMatch(url))
            //{
            //    throw new BaseException("Url格式错误");
            //}
            List<string> htmls = new List<string>();
            List<M3U8DTO> result = new List<M3U8DTO>();

            #region 准备URL
            string url = IM3U8Service.Url;
            htmls.Add(await GetAsync(url + IM3U8Service.DayTop));
            htmls.Add(await GetAsync(url + IM3U8Service.Month));
            htmls.Add(await GetAsync(url + IM3U8Service.MonthLast));
            #endregion

            #region 抓取HTML解析
            foreach (var html in htmls)
            {
                // 取前三页数据
                for (int i = 1; i < 4; i++)
                {
                    /** TODO 示例
                    * var doc = new HtmlDocument();
                    * doc.LoadHtml(html);
                    * var node = doc.DocumentNode.SelectNodes(".//ul[contains(@class,'dropdown-menu')]/li/a");
                    * for (int i = 0; i < node.Count; i++)
                    * {
                    *    var a = node[i].OuterHtml;
                    *    result.Add(new M3U8DTO()
                    *    {
                    *        Title = node[i].GetAttributeValue("href", ""),
                    *        Url = node[i].InnerHtml
                    *    });
                    * }
                    */
                }
            }
            #endregion
            return result;
        }
        private async Task<string> GetAsync(string url)
        {
            HttpRequestMessage request = new(HttpMethod.Get, url);
            var response = await HttpClientFactory.CreateClient().SendAsync(request);
            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
