using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // 1. サーバーの待ち受け設定（すべてのIPアドレスからの接続をポート 8080 で許可）
        HttpListener listener = new HttpListener();

        try
        {
            listener.Prefixes.Add("http://+:8080/");
            listener.Start();
        }
        catch (Exception)
        {
            listener.Close();

            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
        }

        Console.WriteLine("=========================================");
        Console.WriteLine("【模擬・機器1スタブ】が起動しました");
        Console.WriteLine("   ポート番号: 8080");
        Console.WriteLine("=========================================");
        Console.WriteLine("ペアのPC（クライアント）からの通信を待っています...\n");

        while (true)
        {
            // 2. クライアントからの指示（POST）を待つ
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request.HttpMethod == "GET")
            {
                // 3. 送られてきた指示（JSON）を読み取る
                using var reader = new StreamReader(request.InputStream, request.ContentEncoding);//request.InputStreamをバイトから文字に変換
                string jsonString = reader.ReadToEnd(); //JSONの中身をすべてjsonStringに移す


                Console.WriteLine($"[GET指示受信] 時刻: {DateTime.Now:HH:mm:ss}");
                //Console.WriteLine($"[受信データ] {jsonString}");

                // 4. 機器が動作したと見立てて、返事（JSONデータ）を 200 OK で返す
                //string jsonResponse = "{\"status\": \"success\", \"message\": \"機器1の制御に成功しました。\"}";　//返信するJSONの中身
                string jsonResponse = """
                    [
                      {
                        "id": 1,
                        "productName": "Beer",
                        "value": 500,
                        "amount": 1200,
                        "errorCode": null
                      },
                      {
                        "id": 2,
                        "productName": "ShochuHighball",
                        "value": 200,
                        "amount": 5000,
                        "errorCode": "E001"
                      },
                      {
                        "id": 3,
                        "productName": "Highball",
                        "value": 300,
                        "amount": 3000,
                        "errorCode": null
                      },
                      {
                        "id": 4,
                        "productName": "Shochu",
                        "value": 4000,
                        "amount": 1000,
                        "errorCode": "E002"
                      },
                      {
                        "id": 5,
                        "productName": "JapaneseSake",
                        "value": 1500,
                        "amount": 200,
                        "errorCode": null
                      },
                      {
                        "id": 6,
                        "productName": "Whiskey",
                        "value": 10000,
                        "amount": 10,
                        "errorCode": null
                      },
                      {
                        "id": 7,
                        "productName": "Gin",
                        "value": 5000,
                        "amount": 20,
                        "errorCode": null
                      },
                      {
                        "id": 8,
                        "productName": "Tequila",
                        "value": 3000,
                        "amount": 30,
                        "errorCode": null
                      },
                      {
                        "id": 9,
                        "productName": "Rum",
                        "value": 2000,
                        "amount": 40,
                        "errorCode": null
                      },
                      {
                        "id": 10,
                        "productName": "Chamisul",
                        "value": 400,
                        "amount": 1,
                        "errorCode": "E999"
                      }
                    ]
                    """;
                byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);　//jsonResponseをバイト列に変換

                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.OK; // 200 OK
                response.ContentLength64 = buffer.Length;　//jsonResponse(バイト列変換)の文字数

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);　//送るデータ、送るデータの開始位置、送る長さ
                output.Close();　//送信終了

                Console.WriteLine("クライアントへ応答データを返却しました。\n");
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response.Close();
            }
        }
    }
}
