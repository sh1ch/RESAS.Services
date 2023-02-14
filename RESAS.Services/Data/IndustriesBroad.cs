using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RESAS.Services.Data;
/// <summary>
/// <see cref="IndustriesBroad"/> クラスは、産業大分類を表します。
/// </summary>
public class IndustriesBroad : IResas
{
    /// <summary>
    /// SIC (Standard Industrial Classification) コードを取得または設定します。
    /// </summary>
    [JsonPropertyName("sicCode")]
    public string Code { get; set; } = "";
    /// <summary>
    /// SIC (Standard Industrial Classification) 名を取得または設定します。
    /// </summary>
    [JsonPropertyName("sicName")]
    public string Name { get; set; } = "";

    /// <summary>
    /// SIC (Standard Industrial Classification) コードと名称のセットを取得します。
    /// </summary>
    [JsonIgnore]
    public string Text => $"{Code}: {Name}";

    /// <summary>
    /// <see cref="IndustriesBroad"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    public IndustriesBroad()
    {

    }
}
