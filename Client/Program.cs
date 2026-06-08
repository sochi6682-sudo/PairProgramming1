using NLog;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Client;

internal class Program
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    static async Task Main(string[] args)
    {
        Console.WriteLine("クライアント起動 GET送信開始");
        HttpClient client = new HttpClient();

        ProductSummary results;

        try
        {
            logger.Info("GET通信開始");

            Task<HttpResponseMessage> responseTask = client.GetAsync("http://localhost:8080/");
            Task timeoutTask = Task.Delay(10000);

            Task completedTask = await Task.WhenAny(responseTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                Console.WriteLine("GET通信がタイムアウトしました");
                logger.Error($"GET通信タイムアウト");
                return;
            }
            else
            {
                HttpResponseMessage response = await responseTask;
                logger.Info($"GET通信成功 StatusCode={(int)response.StatusCode}");

                string result = await response.Content.ReadAsStringAsync();

                var analyzer = new DataAnalyzer();
                results = analyzer.Analyzer(result);
            }
        }

        catch(JsonException ex)
        {
            Console.WriteLine("JSON解析エラー");
            logger.Error(ex, "JSON解析エラー");
            return;
        }

        catch (Exception ex)
        {
            Console.WriteLine("通信に失敗しました");
            logger.Error(ex, "GET通信失敗");
            return;
        }

        // 集計結果の表示
        Console.WriteLine();
        Console.WriteLine("=== 集計結果 ===");
        Console.WriteLine();
        Console.WriteLine($"全データ件数：{results.TotalCount}件");
        Console.WriteLine($"正常データ：{results.NormalCount}件");
        Console.WriteLine($"異常データ：{results.ErrorCount}件");

        // 合計金額の表示
        Console.WriteLine($"合計値金額：{results.TotalValue:N0}円");
        Console.WriteLine();

        // 単価TOP5の表示
        Console.WriteLine("=== 単価TOP5 ===");
        Console.WriteLine();

        foreach (var item in results.Top5Products)
        {
            Console.WriteLine($"{item.ProductName} {item.Value}");
        }
        // エラーデータ一覧表示
        Console.WriteLine();
        Console.WriteLine("=== エラーデータ一覧 ===");
        Console.WriteLine();

        foreach (var item in results.ErrorProducts)
        {
            Console.WriteLine($"ID:{item.Id} Product:{item.ProductName} Value:{item.Value} Error:{item.ErrorCode}");
        }
    }
}