using NLog;
using StubServer.Data;
using StubServer.Models;
using System.Net;
using System.Text;
using System.Text.Json;


class Program
{
    //ロガー作成
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        //①サーバー公開処理

        // サーバーの待ち受け設定（すべてのIPアドレスからの接続をポート 8080 で許可）
        HttpListener listener = new HttpListener();
        logger.Info("サーバーアプリ起動");

        try
        {
            //外部公開でサーバー起動
            listener.Prefixes.Add("http://+:8080/");
            listener.Start();

            logger.Info("外部公開起動");
            logger.Info("待ち受けURL=http://+:8080/");

        }
        catch (Exception ex)
        {
            logger.Warn(ex, "外部公開起動に失敗");

            try
            {
                //外部公開できなければ一度開いた物を閉じて
                listener.Close();
                //ローカルで公開
                listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:8080/");
                listener.Start();

                logger.Info("localhost起動");
            }
            catch (Exception ex2)
            {
                logger.Error(ex2, "localhost起動にも失敗");

                Console.WriteLine("サーバー起動に失敗しました。");
                Console.WriteLine("Enterキーで終了します。");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        Console.WriteLine("=========================================");
        Console.WriteLine("【模擬・機器1スタブ】が起動しました");
        Console.WriteLine("   ポート番号: 8080");
        Console.WriteLine("=========================================");
        Console.WriteLine("ペアのPC（クライアント）からの通信を待っています...\n");


        //②リクエスト処理
        while (true)
        {
            // クライアントからの指示を待つ
            logger.Info("リクエスト待機中");
            HttpListenerContext context = listener.GetContext();

            //ContextをRequestとResponseに分ける
            logger.Info("リクエスト受信");
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            // 送られてきた指示（JSON）を読み取る
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);//request.InputStreamをバイトから文字に変換
            string jsonString = reader.ReadToEnd(); //JSONの中身をすべてjsonStringに移す

            //接続元のIPアドレスとポート番号、HTTPメソッド、アクセスされたURLをロギング
            logger.Info($"受信 Client={request.RemoteEndPoint} Method={request.HttpMethod} Url={request.Url}");


            if (request.HttpMethod == "GET")
            {
                
                Console.WriteLine("=========================================");
                Console.WriteLine($"[GET指示受信] 時刻: {DateTime.Now:HH:mm:ss}");

                //スタブデータをバイト列に変換
                string jsonResponse = ProductData.GetJson();//スタブデータを読出して
                byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse); //バイト列に変換

                // 返事をJSON形式、ステータスコード200 OK に設定
                response.ContentType = "application/json";　//JSON形式
                response.StatusCode = (int)HttpStatusCode.OK; // 200 OK
                response.ContentLength64 = buffer.Length;　//jsonResponse(バイト列変換)の文字数

                try
                {
                    //返信処理(200)
                    Stream output = response.OutputStream; //返信データを書き込む場所を取得
                    output.Write(buffer, 0, buffer.Length); //返信開始（送るデータ、送るデータの開始位置、送る長さ）
                    output.Close(); //返信終了

                    logger.Info($"GET返信正常完了");
                    Console.WriteLine("=========================================");
                    Console.WriteLine("GET返信正常完了\n");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GET返信失敗");
                    Console.WriteLine("=========================================");
                    Console.WriteLine("GET返信失敗\n");
                }

            }
            else　//GET以外が来たら
            {
                //返信処理(405)
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response.Close();

                //HTTPメソッド、アクセスされたURLをロギング
                logger.Warn($"未許可メソッド受信 Method={request.HttpMethod} Url={request.Url}");
                Console.WriteLine("=========================================");
                Console.WriteLine("未許可メソッド受信\n");
                

            }
        }
    }
}
