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
                PropertyNameCaseInsensitive = true  //大文字と小文字を区別せずに比較する
            };

            List<ProductData> list = JsonSerializer.Deserialize<List<ProductData>>(jsonResponse, options);

            //タプルでinitialに初期値をまとめる
            var initial = (
            TotalValue: 0L, //合計金額(初期値0円)
            TotalCount: 0,  //全件数(初期値0件)
            ErrorCount: 0,  //エラー件数(初期値0件)
            NormalCount: 0, //正常件数(初期値0件)
            ErrorList: new List<ProductData>() // エラー品をいれる
        );

            var aggResult = list.Aggregate(
                initial,                  //初期値
                (result, current) => {    //result = result + current 集計
                    // エラーがある場合はリストに追加
                    if (current.ErrorCode != null)
                    {
                        result.ErrorList.Add(current);
                    }


                    //=>の後に中括弧 { }がある場合はreturnが必要になる。
                    //中括弧 { }が無い場合は自動でreturnされるため不要
                    return (
                        TotalValue: result.TotalValue + (current.Value * current.Amount),            //合計金額集計 合計金額 + (単価×数量)
                        TotalCount: result.TotalCount + 1,                                     //全件数集計　合計件数 + 1件
                        ErrorCount: result.ErrorCount + (current.ErrorCode != null ? 1 : 0),      //エラー件数集計　合計エラー件数 + (エラーで 1、正常で 0)
                        NormalCount: result.NormalCount + (current.ErrorCode == null ? 1 : 0),    //正常件数集計　合計正常件数 + (正常で 1、エラーで 0)
                        ErrorList: result.ErrorList                                            //エラーがあるデータ
                    );
                }
            );

            //リストを単価順に並び替えて上から5件取得
            var top5 = list
                .OrderByDescending(p => p.Value)
                .Take(5)
                .ToList();

            //結果をrecordのProductSummary型で返す
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

    //集計データをrecordでまとめる
    public record ProductSummary(
        long TotalValue,
        int TotalCount,
        int ErrorCount,
        int NormalCount,
        List<ProductData> Top5Products,
        List<ProductData> ErrorProducts
    );
}
