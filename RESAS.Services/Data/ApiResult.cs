using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RESAS.Services.Data;

/// <summary>
/// <see cref="ApiResult{T}"/> クラスは、RESAS API レスポンス データフォーマットを表現します。
/// </summary>
public class ApiResult<T>
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("result")]
    public IEnumerable<T>? Result { get; set; }
}

/// <summary>
/// <see cref="IResas"/> インターフェースは、RESAS API データを意味します。
/// </summary>
public interface IResas
{

}
