using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RESAS.Services.Data;

/// <summary>
/// <see cref="Prefecture"/> クラスは、都道府県コードを表現します。
/// </summary>
public class Prefecture : IResas
{
    /// <summary>
    /// 都道府県コードを取得または設定します。
    /// </summary>
    [JsonPropertyName("prefCode")]
    public int Code { get; set; } = 0;
    /// <summary>
    /// 都道府県名を取得または設定します。
    /// </summary>
    [JsonPropertyName("prefName")]
    public string Name { get; set; } = "";

    /// <summary>
    /// <see cref="Prefecture"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    public Prefecture()
    {

    }
}
