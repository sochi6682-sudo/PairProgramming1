using Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Client
{
    public class DataAnalyzer
    {
        public ProductSummary Analyzer(string jsonResponse) 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<ProductData> todoList = JsonSerializer.Deserialize<List<ProductData>>(jsonResponse, options);

            var initialSeed = (
            TotalValue: 0L,
            TotalCount: 0,
            ErrorCount: 0,
            NormalCount: 0,
            ErrorList: new List<ProductData>() // ループ中にエラー品をここに放り込む
        );

            var aggResult = todoList.Aggregate(
                initialSeed,
                (acc, item) => {
                    // エラーがある場合はリストに追加
                    if (item.ErrorCode != null)
                    {
                        acc.ErrorList.Add(item);
                    }

                    return (
                        TotalValue: acc.TotalValue + (item.Value * item.Amount),
                        TotalCount: acc.TotalCount + 1,
                        ErrorCount: acc.ErrorCount + (!string.IsNullOrEmpty(item.ErrorCode) ? 1 : 0),
                        NormalCount: acc.NormalCount + (string.IsNullOrEmpty(item.ErrorCode) ? 1 : 0),
                        ErrorList: acc.ErrorList
                    );
                }
            );

            var top5 = todoList
                .OrderByDescending(p => p.Value * p.Amount)
                .Take(5)
                .ToList();

            return new ProductSummary(
                aggResult.TotalValue,
                aggResult.TotalCount,
                aggResult.ErrorCount,
                aggResult.NormalCount,
                top5,
                aggResult.ErrorList
            );
        }
    }

    public record ProductSummary(
        long TotalValue,
        int TotalCount,
        int ErrorCount,
        int NormalCount,
        List<ProductData> Top5Products,
        List<ProductData> ErrorProducts
    );
}
