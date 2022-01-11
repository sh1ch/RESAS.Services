using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESAS.Services;

/// <summary>
/// <see cref="ResasApi"/> 列挙型は、API キーを示します。
/// </summary>
public enum ResasApi
{
    /// <summary>
    /// 都道府県コード。
    /// </summary>
    Prefectures,
    /// <summary>
    /// 産業大分類。
    /// </summary>
    IndustriesBroad,
}
