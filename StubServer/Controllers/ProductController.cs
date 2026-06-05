using NLog;
using StubServer.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StubServer.Controllers;

public static class ProductController
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public static void Handle(HttpListenerContext context)
    {
        var request = context.Request;

        logger.Info($"受信 Client={request.RemoteEndPoint} Method={request.HttpMethod} Url={request.Url}");

        switch (request.HttpMethod)
        {
            case "GET":
                HandleGet(context);
                break;

            case "POST":
                HandlePost(context);
                break;

            default:
                HandleMethodNotAllowed(context);
                break;
        }
    }

    private static void HandleGet(HttpListenerContext context)
    {
        string jsonResponse = ProductData.GetJson();
        byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.ContentLength64 = buffer.Length;

        using Stream output = context.Response.OutputStream;

        output.Write(buffer, 0, buffer.Length);

        logger.Info($"GET返信正常完了 件数=10 Bytes={buffer.Length}");
    }

    private static void HandlePost(HttpListenerContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Created;
        context.Response.Close();

        logger.Info("POST受信");
    }

    private static void HandleMethodNotAllowed(HttpListenerContext context)
    {
        context.Response.StatusCode =(int)HttpStatusCode.MethodNotAllowed;
        context.Response.Close();

        logger.Warn($"未許可メソッド受信 Method={context.Request.HttpMethod}");
    }
}
