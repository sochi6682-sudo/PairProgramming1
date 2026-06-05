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
        Console.WriteLine(result);


    }
}
