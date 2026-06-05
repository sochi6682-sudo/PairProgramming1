using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using StubServer.Controllers;

namespace StubServer.Server;

public class HttpStubServer
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    private HttpListener _listener = new();

    public void Start()
    {
        logger.Info("サーバーアプリ起動");

        try
        {
            _listener.Prefixes.Add("http://+:8080/");
            _listener.Start();

            logger.Info("外部公開起動");
            logger.Info("待ち受けURL=http://+:8080/");
        }
        catch (Exception ex)
        {
            logger.Warn(ex, "外部公開起動に失敗");

            _listener.Close();

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8080/");
            _listener.Start();

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
            var context = _listener.GetContext();

            ProductController.Handle(context);
        }
    }
}
