namespace Parseweather.Models
{
    public class WeatherResponse
    {
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 溫度
        /// </summary>
        public string Temperature { get; set; }
        /// <summary>
        /// 降雨機率
        /// </summary>
        public string RainPercent { get; set; }
        /// <summary>
        /// 圖片顯示位置
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 說明
        /// </summary>
        public string Explain { get; set; }
   
    }
}