using System.Reflection.Metadata;

namespace Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("クライアント起動 GET送信開始");
        HttpClient client = new HttpClient();

        var response = await client.GetAsync("http://localhost:8080/");
        string result = await response.Content.ReadAsStringAsync();

        var analyzer = new DataAnalyzer();
        ProductSummary results = analyzer.Analyzer(result);

        Console.WriteLine(results.ErrorCount);

        // 1. === 集計結果 === の表示
        Console.WriteLine("=== 集計結果 ===");
        Console.WriteLine(); // 空行
        Console.WriteLine($"全データ件数：{results.TotalCount}件");
        Console.WriteLine($"正常データ：{results.NormalCount}件");
        Console.WriteLine($"異常データ：{results.ErrorCount}件");

        // 💡 正常データの合計金額を、3桁カンマ区切り（2,170,000）で表示するおまじない ":N0"
        // もし全体の合計金額なら result.TotalValue に変えてください
        Console.WriteLine($"正常データ合計値：{results.TotalValue:N0}");
        Console.WriteLine();
        Console.WriteLine(); // 2行空ける

        // 2. === value降順TOP5 === の表示
        Console.WriteLine("=== value降順TOP5 ===");
        Console.WriteLine();

        foreach (var item in results.Top5Products)
        {
            Console.WriteLine($"{item.ProductName} {item.Value}");
            Console.WriteLine(); // 商品ごとに1行空ける
        }
        Console.WriteLine(); // 2行空ける

        // 3. === エラーデータ一覧 === の表示
        Console.WriteLine("=== エラーデータ一覧 ===");
        Console.WriteLine();

        foreach (var item in results.ErrorProducts)
        {
            // 💡 ID、商品名、単価、エラーコードを綺麗に並べる
            Console.WriteLine($"ID:{item.Id} Product:{item.ProductName} Value:{item.Value} Error:{item.ErrorCode}");
            Console.WriteLine();


        }
    }
}
