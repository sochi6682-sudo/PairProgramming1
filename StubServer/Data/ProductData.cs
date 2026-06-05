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

            new Product { Id = 10, ProductName = "Chamisul", Value = 400, Amount = 1, ErrorCode = "E999" },

            new Product { Id = 11, ProductName = "WineRed", Value = 2500, Amount = 150, ErrorCode = null },

            new Product { Id = 12, ProductName = "WineWhite", Value = 2300, Amount = 180, ErrorCode = null },

            new Product { Id = 13, ProductName = "Champagne", Value = 12000, Amount = 15, ErrorCode = "E003" },

            new Product { Id = 14, ProductName = "Brandy", Value = 8000, Amount = 25, ErrorCode = null },

            new Product { Id = 15, ProductName = "Vodka", Value = 1800, Amount = 60, ErrorCode = null },

            new Product { Id = 16, ProductName = "Cider", Value = 150, Amount = 2000, ErrorCode = null },

            new Product { Id = 17, ProductName = "LemonSour", Value = 160, Amount = 4500, ErrorCode = null },

            new Product { Id = 18, ProductName = "GrapefruitSour", Value = 160, Amount = 3000, ErrorCode = "E001" },

            new Product { Id = 19, ProductName = "Umeshu", Value = 1200, Amount = 400, ErrorCode = null },

            new Product { Id = 20, ProductName = "Makgeolli", Value = 600, Amount = 350, ErrorCode = null },

            new Product { Id = 21, ProductName = "BeerPremium", Value = 300, Amount = 800, ErrorCode = null },

            new Product { Id = 22, ProductName = "BeerDraft", Value = 220, Amount = 1500, ErrorCode = null },

            new Product { Id = 23, ProductName = "BeerStout", Value = 350, Amount = 200, ErrorCode = null },

            new Product { Id = 24, ProductName = "NonAlcoholBeer", Value = 140, Amount = 1000, ErrorCode = null },

            new Product { Id = 25, ProductName = "RedWinePremium", Value = 25000, Amount = 5, ErrorCode = null },

            new Product { Id = 26, ProductName = "WhiteWineChablis", Value = 5500, Amount = 30, ErrorCode = "E004" },

            new Product { Id = 27, ProductName = "RoseWine", Value = 1800, Amount = 90, ErrorCode = null },

            new Product { Id = 28, ProductName = "SparklingWine", Value = 2000, Amount = 500, ErrorCode = null },

            new Product { Id = 29, ProductName = "Sherry", Value = 3200, Amount = 40, ErrorCode = null },

            new Product { Id = 30, ProductName = "Vermouth", Value = 2100, Amount = 70, ErrorCode = null },

            new Product { Id = 31, ProductName = "SingleMaltWhiskey", Value = 9500, Amount = 50, ErrorCode = null },

            new Product { Id = 32, ProductName = "BlendedWhiskey", Value = 4500, Amount = 120, ErrorCode = null },

            new Product { Id = 33, ProductName = "BourbonWhiskey", Value = 3800, Amount = 80, ErrorCode = "E101" },

            new Product { Id = 34, ProductName = "ScotchWhiskey", Value = 6000, Amount = 45, ErrorCode = null },

            new Product { Id = 35, ProductName = "JapaneseWhiskey", Value = 15000, Amount = 15, ErrorCode = null },

            new Product { Id = 36, ProductName = "TaiwaneseWhiskey", Value = 11000, Amount = 10, ErrorCode = null },

            new Product { Id = 37, ProductName = "IrishWhiskey", Value = 3500, Amount = 65, ErrorCode = null },

            new Product { Id = 38, ProductName = "AppleBrandy", Value = 7200, Amount = 20, ErrorCode = null },

            new Product { Id = 39, ProductName = "Cognac", Value = 18000, Amount = 8, ErrorCode = null },

            new Product { Id = 40, ProductName = "Armagnac", Value = 14000, Amount = 12, ErrorCode = "E102" },

            new Product { Id = 41, ProductName = "DryGin", Value = 2400, Amount = 250, ErrorCode = null },

            new Product { Id = 42, ProductName = "CraftGin", Value = 5800, Amount = 40, ErrorCode = null },

            new Product { Id = 43, ProductName = "PinkGin", Value = 2800, Amount = 60, ErrorCode = null },

            new Product { Id = 44, ProductName = "WhiteRum", Value = 1900, Amount = 180, ErrorCode = null },

            new Product { Id = 45, ProductName = "DarkRum", Value = 3100, Amount = 95, ErrorCode = null },

            new Product { Id = 46, ProductName = "SpicedRum", Value = 2600, Amount = 110, ErrorCode = null },

            new Product { Id = 47, ProductName = "TequilaBlanco", Value = 3500, Amount = 75, ErrorCode = "E201" },

            new Product { Id = 48, ProductName = "TequilaReposado", Value = 5200, Amount = 40, ErrorCode = null },

            new Product { Id = 49, ProductName = "TequilaAnejo", Value = 8500, Amount = 18, ErrorCode = null },

            new Product { Id = 50, ProductName = "Mezcal", Value = 6400, Amount = 22, ErrorCode = null },

            new Product { Id = 51, ProductName = "PremiumSake_Daiginjo", Value = 5000, Amount = 100, ErrorCode = null },

            new Product { Id = 52, ProductName = "Sake_Junmai", Value = 1200, Amount = 600, ErrorCode = null },

            new Product { Id = 53, ProductName = "Sake_Honjozo", Value = 950, Amount = 800, ErrorCode = null },

            new Product { Id = 54, ProductName = "NigoriSake", Value = 1300, Amount = 150, ErrorCode = null },

            new Product { Id = 55, ProductName = "SparklingSake", Value = 450, Amount = 1200, ErrorCode = "E301" },

            new Product { Id = 56, ProductName = "Shochu_Imo", Value = 2500, Amount = 450, ErrorCode = null },

            new Product { Id = 57, ProductName = "Shochu_Mugi", Value = 2100, Amount = 550, ErrorCode = null },

            new Product { Id = 58, ProductName = "Shochu_Kome", Value = 2400, Amount = 200, ErrorCode = null },

            new Product { Id = 59, ProductName = "Awamori", Value = 3000, Amount = 180, ErrorCode = null },

            new Product { Id = 60, ProductName = "CassisLiqueur", Value = 1800, Amount = 320, ErrorCode = null },

            new Product { Id = 61, ProductName = "PeachLiqueur", Value = 1600, Amount = 280, ErrorCode = null },

            new Product { Id = 62, ProductName = "KahluaCoffeeLiqueur", Value = 1900, Amount = 400, ErrorCode = null },

            new Product { Id = 63, ProductName = "MatchaLiqueur", Value = 2200, Amount = 90, ErrorCode = "E401" },

            new Product { Id = 64, ProductName = "Campari", Value = 2400, Amount = 150, ErrorCode = null },

            new Product { Id = 65, ProductName = "Absinthe", Value = 5500, Amount = 35, ErrorCode = null },

            new Product { Id = 66, ProductName = "ShaoxingWine", Value = 1500, Amount = 250, ErrorCode = null },

            new Product { Id = 67, ProductName = "Grappa", Value = 4800, Amount = 50, ErrorCode = null },

            new Product { Id = 68, ProductName = "Calvados", Value = 6500, Amount = 28, ErrorCode = null },

            new Product { Id = 69, ProductName = "Limoncello", Value = 2700, Amount = 120, ErrorCode = null },

            new Product { Id = 70, ProductName = "Sangria", Value = 1100, Amount = 650, ErrorCode = null },

            new Product { Id = 71, ProductName = "HighballCan_Strong", Value = 210, Amount = 5000, ErrorCode = null },

            new Product { Id = 72, ProductName = "ChuhaiCan_Lemon", Value = 150, Amount = 8000, ErrorCode = null },

            new Product { Id = 73, ProductName = "ChuhaiCan_Grape", Value = 150, Amount = 4200, ErrorCode = null },

            new Product { Id = 74, ProductName = "ChuhaiCan_Peach", Value = 150, Amount = 3500, ErrorCode = "E501" },

            new Product { Id = 75, ProductName = "HighballCan_Premium", Value = 320, Amount = 1500, ErrorCode = null },

            new Product { Id = 76, ProductName = "HardCiderCan", Value = 280, Amount = 900, ErrorCode = null },

            new Product { Id = 77, ProductName = "TestBeer_DummyData", Value = 10, Amount = 9999, ErrorCode = "E999" },

            new Product { Id = 78, ProductName = "SampleWine_NoStock", Value = 3000, Amount = 0, ErrorCode = null },

            new Product { Id = 79, ProductName = "ExpiredProduct", Value = 500, Amount = 50, ErrorCode = "E080" },

            new Product { Id = 80, ProductName = "BrokenPackageItem", Value = 1200, Amount = 12, ErrorCode = "E081" },

            new Product { Id = 81, ProductName = "BeerLocalCraft_A", Value = 600, Amount = 400, ErrorCode = null },

            new Product { Id = 82, ProductName = "BeerLocalCraft_B", Value = 650, Amount = 350, ErrorCode = null },

            new Product { Id = 83, ProductName = "BeerLocalCraft_C", Value = 700, Amount = 200, ErrorCode = null },

            new Product { Id = 84, ProductName = "PremiumVodka", Value = 4500, Amount = 85, ErrorCode = null },

            new Product { Id = 85, ProductName = "FlavoredVodka_Citrus", Value = 2200, Amount = 140, ErrorCode = null },

            new Product { Id = 86, ProductName = "HoneyLiqueur", Value = 3400, Amount = 65, ErrorCode = null },

            new Product { Id = 87, ProductName = "CoconutRum", Value = 2100, Amount = 220, ErrorCode = null },

            new Product { Id = 88, ProductName = "IrishCream", Value = 2700, Amount = 190, ErrorCode = "E402" },

            new Product { Id = 89, ProductName = "Ame_No_Uo_Sake", Value = 1800, Amount = 30, ErrorCode = null },

            new Product { Id = 90, ProductName = "HotSake_Tokkuri", Value = 850, Amount = 450, ErrorCode = null },

            new Product { Id = 91, ProductName = "Okinawa_Awamori_Old", Value = 7500, Amount = 40, ErrorCode = null },

            new Product { Id = 92, ProductName = "HighValueCollectorWhiskey", Value = 85000, Amount = 2, ErrorCode = null },

            new Product { Id = 93, ProductName = "PubHouseWine_Bulk", Value = 8500, Amount = 80, ErrorCode = null },

            new Product { Id = 94, ProductName = "NonAlcoholWine_Red", Value = 980, Amount = 300, ErrorCode = null },

            new Product { Id = 95, ProductName = "NonAlcoholWine_White", Value = 980, Amount = 250, ErrorCode = null },

            new Product { Id = 96, ProductName = "GingerAle_Mixer", Value = 120, Amount = 6000, ErrorCode = null },

            new Product { Id = 97, ProductName = "TonicWater_Mixer", Value = 120, Amount = 6500, ErrorCode = null },

            new Product { Id = 98, ProductName = "ClubSoda_Mixer", Value = 100, Amount = 8000, ErrorCode = null },

            new Product { Id = 99, ProductName = "SystemErrorDummyItem", Value = 1, Amount = 1, ErrorCode = "E999" },

            new Product { Id = 100, ProductName = "FinishingTequilaGold", Value = 15000, Amount = 10, ErrorCode = null }

        };

        return JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,WriteIndented = true
        });
    }
}
