using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task CallExe(List<M3U8DTO> m3u8)
        {
            foreach (var item in m3u8)
            {
                await Task.Run(() =>
                {
                    Process exe = new Process();
                    var rootDirectory = Environment.CurrentDirectory.ToString();
                    exe.StartInfo.FileName = rootDirectory+"\\N_m3u8DL-CLI_v3.0.2.exe";
                    exe.StartInfo.Arguments = item.M3U8Url + " --saveName " + item.Title + " --workDir "+rootDirectory.Substring(0,1)+":/pro --enableDelAfterDone";
                    exe.Start();
                    exe.WaitForExit();
                });
            }
        }

        public async Task<List<M3U8DTO>> DowM3U8(string url)
        {
            #region URL 校验
            Regex httpRegex = new Regex(@"[a-zA-z]+://[^\s]*");
            if (!httpRegex.IsMatch(url))
            {
                throw new BaseException("Url格式错误");
            }
            #endregion

            List<M3U8DTO> result = new List<M3U8DTO>();

            #region 抓取HTML解析
            // 取前三页数据
            for (int i = 1; i < 4; i++)
            {
                string urlPage = url + "/" + i;
                var html = await GetAsync(urlPage);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var colVideoList = doc.DocumentNode.SelectNodes(".//div[contains(@class,'colVideoList')]");
                for (int j = 0; j < colVideoList.Count; j++)
                {
                    var doc1 = new HtmlDocument();
                    doc1.LoadHtml(colVideoList[j].OuterHtml);
                    var node = doc1.DocumentNode.SelectSingleNode(".//a[2]");
                    var video = result.FirstOrDefault(n => n.Url == node.GetAttributeValue("href", ""));
                    if (video != null)
                    {
                        continue;
                    }
                    M3U8DTO m3 = new M3U8DTO()
                    {
                        Title = node.GetAttributeValue("title", new Random().Next(1, 9).ToString()),
                        Url = node.GetAttributeValue("href", ""),
                    };
                    var vhtml = await GetAsync("https://91porny.com" + m3.Url);
                    var vdoc = new HtmlDocument();
                    vdoc.LoadHtml(vhtml);
                    var m3u8 = vdoc.DocumentNode.SelectSingleNode("//video");
                    m3.M3U8Url = m3u8.GetAttributeValue("data-src", "");
                    m3.M3U8Url = m3.M3U8Url.Replace("amp;","&");
                    if (string.IsNullOrEmpty(m3.M3U8Url))
                    {
                        continue;
                    }
                    Process exe = new Process();
                    var rootDirectory = Environment.CurrentDirectory.ToString();
                    exe.StartInfo.FileName = rootDirectory+"\\N_m3u8DL-CLI_v3.0.2.exe";
                    exe.StartInfo.Arguments = m3.M3U8Url + " --saveName " + m3.Title + " --workDir "+rootDirectory.Substring(0,1)+":/pro --enableDelAfterDone";
                    exe.Start();
                    exe.WaitForExit();
                    result.Add(m3);
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
