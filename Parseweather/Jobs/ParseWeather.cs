using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Parseweather.Models;
using Quartz;

namespace Parseweather.Jobs
{
    public class ParseWeather : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"job start {DateTime.Now:yyyyMMdd-HHmm}");

            var config = Configuration.Default.WithDefaultLoader();
            const string url = "https://www.cwb.gov.tw/V7/forecast/f_index.htm";
            using (var dom = await BrowsingContext.New(config).OpenAsync(url))
            {
                //取出網頁上所有縣市ID
                var parseCityIds = dom
                    .QuerySelectorAll("body > div > div > div > table > tbody > tr")
                    .Select(s => s.GetAttribute("id")).Where(w => w != null);
                //針對id向下擷取個內容
                var result = parseCityIds.Select(item => new WeatherResponse
                {
                    City = dom.QuerySelector($"#{item} > td:nth-child(1)").Text(),
                    Temperature = dom.QuerySelector($"#{item} > td:nth-child(2) > a").Text(),
                    RainPercent = dom.QuerySelector($"#{item} > td:nth-child(3) > a").Text(),
                    ImageUrl = dom.QuerySelector($"#{item} > td:nth-child(4) > a > div > img").GetAttribute("src"),
                    Explain = dom.QuerySelector($"#{item} > td:nth-child(4) > a > div > img").GetAttribute("title")
                });

                foreach (var item in result)
                    Console.WriteLine(
                        $"城市：{item.City}  溫度：{item.Temperature}  降雨機率：{item.RainPercent} 圖片位置：{item.ImageUrl}  說明：{item.Explain}{Environment.NewLine}");
            }

            Console.WriteLine($"job Leave {DateTime.Now:yyyyMMdd-HHmm}");
        }
    }
}