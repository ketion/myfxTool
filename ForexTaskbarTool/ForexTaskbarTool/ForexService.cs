using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForexTaskbarTool
{
    public class ForexData
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
    }

    public class ForexService
    {
        private readonly HttpClient _httpClient;

        public ForexService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        }

        public async Task<ForexData> GetForexDataAsync(string symbol)
        {
            try
            {
                string url = $"https://widget.myfxbook.com/widget/market-quotes.html?symbols={symbol}";
                string html = await _httpClient.GetStringAsync(url);

                // 从HTML中提取价格和变化数据
                var priceMatch = Regex.Match(html, @"<span[^>]*class=""[^""]*price[^""]*""[^>]*>([0-9.]+)</span>");
                var changeMatch = Regex.Match(html, @"<span[^>]*class=""[^""]*change[^""]*""[^>]*>([+-]?[0-9.]+)</span>");

                if (!priceMatch.Success)
                {
                    // 尝试另一种模式
                    priceMatch = Regex.Match(html, @"""price""[:\s]*""?([0-9.]+)""?");
                    changeMatch = Regex.Match(html, @"""change""[:\s]*""?([+-]?[0-9.]+)""?");
                }

                if (priceMatch.Success)
                {
                    decimal price = decimal.Parse(priceMatch.Groups[1].Value);
                    decimal change = changeMatch.Success ? decimal.Parse(changeMatch.Groups[1].Value) : 0;

                    return new ForexData
                    {
                        Symbol = symbol,
                        Price = price,
                        Change = change
                    };
                }

                // 如果HTML解析失败，尝试使用备用API
                return await GetForexDataFromBackupAsync(symbol);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<ForexData> GetForexDataFromBackupAsync(string symbol)
        {
            try
            {
                // 使用备用数据源（示例）
                string apiUrl = $"https://www.myfxbook.com/api/get-community-outlook.json?symbols={symbol}";
                string response = await _httpClient.GetStringAsync(apiUrl);
                
                // 简单解析（实际需要根据API响应格式调整）
                var priceMatch = Regex.Match(response, @"""price""[:\s]*([0-9.]+)");
                if (priceMatch.Success)
                {
                    return new ForexData
                    {
                        Symbol = symbol,
                        Price = decimal.Parse(priceMatch.Groups[1].Value),
                        Change = 0
                    };
                }
            }
            catch { }

            return null;
        }
    }
}
