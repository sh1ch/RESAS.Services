using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESAS.Services;

/// <summary>
/// <see cref="ResasEngine"/> クラスは、RESAS を利用するための基本システムを管理します。
/// </summary>
public static class ResasEngine
{
    /// <summary>
    /// RESAS を利用するための API キーを取得または設定します。
    /// </summary>
    public static string ApiKey { get; set; } = "";
}
