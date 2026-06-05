using NLog;
using StubServer.Data;
using StubServer.Server;
using System;
using System.IO;
using System.Net;
using System.Text;


class Program
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {

        // 1. サーバーの待ち受け設定（すべてのIPアドレスからの接続をポート 8080 で許可）
        HttpListener listener = new HttpListener();
        logger.Info("サーバーアプリ起動");

        try
        {
            listener.Prefixes.Add("http://+:8080/");
            listener.Start();
            logger.Info("外部公開起動");
            logger.Info("待ち受けURL=http://+:8080/");
        }
        catch (Exception ex)
        {
            logger.Warn(ex, "外部公開起動に失敗");

            listener.Close();

            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            logger.Info("localhost起動");
            logger.Info("待ち受けURL=http://localhost:8080/");
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
            
            // 3. 送られてきた指示（JSON）を読み取る
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);//request.InputStreamをバイトから文字に変換
            string jsonString = reader.ReadToEnd(); //JSONの中身をすべてjsonStringに移す
            logger.Info($"受信 Client={request.RemoteEndPoint} Method={request.HttpMethod} Url={request.Url}");


            if (request.HttpMethod == "GET")
            {

                Console.WriteLine("=========================================");
                Console.WriteLine($"[GET指示受信] 時刻: {DateTime.Now:HH:mm:ss}");

                // 4. 機器が動作したと見立てて、返事（JSONデータ）を 200 OK で返す

                string jsonResponse = ProductData.GetJson();
                byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);　//jsonResponseをバイト列に変換

                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.OK; // 200 OK
                response.ContentLength64 = buffer.Length;　//jsonResponse(バイト列変換)の文字数

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);　//送るデータ、送るデータの開始位置、送る長さ
                output.Close();　//送信終了

                Console.WriteLine("クライアントへ応答データを返却しました。\n");
                logger.Info($"GET返信正常完了 件数=10 Bytes={buffer.Length}");
            }
            else
            {
               
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response.Close();

                logger.Warn($"未許可メソッド受信 Method={request.HttpMethod} Url={request.Url}");
            }
        }
    }
}
