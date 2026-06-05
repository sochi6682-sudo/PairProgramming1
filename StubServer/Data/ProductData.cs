using StubServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StubServer.Data;

public static class ProductData
{
    public static string GetJson()
    {
        var products = new[]
        {
            new Product { Id = 1, ProductName = "Beer", Value = 500, Amount = 1200, ErrorCode = null },
            new Product { Id = 2, ProductName = "ShochuHighball", Value = 200, Amount = 5000, ErrorCode = "E001" },
            new Product { Id = 3, ProductName = "Highball", Value = 300, Amount = 3000, ErrorCode = null },
            new Product { Id = 4, ProductName = "Shochu", Value = 4000, Amount = 1000, ErrorCode = "E002" },
            new Product { Id = 5, ProductName = "JapaneseSake", Value = 1500, Amount = 200, ErrorCode = null },
            new Product { Id = 6, ProductName = "Whiskey", Value = 10000, Amount = 10, ErrorCode = null },
            new Product { Id = 7, ProductName = "Gin", Value = 5000, Amount = 20, ErrorCode = null },
            new Product { Id = 8, ProductName = "Tequila", Value = 3000, Amount = 30, ErrorCode = null },
            new Product { Id = 9, ProductName = "Rum", Value = 2000, Amount = 40, ErrorCode = null },
            new Product { Id = 10, ProductName = "Chamisul", Value = 400, Amount = 1, ErrorCode = "E999" }
        };

        return JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,WriteIndented = true
        });
    }
}
