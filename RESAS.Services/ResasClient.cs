using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using RESAS.Services.Data;

namespace RESAS.Services;

/// <summary>
/// <see cref="ResasClient"/> クラスは、RESAS API 用のクライアント接続を提供します。
/// </summary>
public class ResasClient
{
    private const string _BaseUrl = "https://opendata.resas-portal.go.jp/";

    /// <summary>
    /// 都道府県コードの URI に API 要求を非同期操作として送信します。(API キーは <see cref="ResasEngine"/> を利用します)
    /// </summary>
    /// <returns>都道府県コードのコレクション。取得に失敗した場合、空のコレクションを返却します。</returns>
    public static async Task<IEnumerable<Prefecture>> GetPrefecturesAsync() => await GetPrefecturesAsync(ResasEngine.ApiKey);

    /// <summary>
    /// 都道府県コードの URI に API 要求を非同期操作として送信します。
    /// </summary>
    /// <param name="key">API キー。</param>
    /// <returns>都道府県コードのコレクション。取得に失敗した場合、空のコレクションを返却します。</returns>
    public static async Task<IEnumerable<Prefecture>> GetPrefecturesAsync(string key)
    {
        string prefecturesUri = "api/v1/prefectures";
        string url = _BaseUrl + prefecturesUri;
        string response = await GetHttpResponce(key, url);

        ApiResult<Prefecture>? result = null;

        if (response != null)
        {
            var options = new JsonSerializerOptions();

            result = JsonSerializer.Deserialize<ApiResult<Prefecture>>(response, options);
        }

        return result?.Result ?? new List<Prefecture>();
    }

    /// <summary>
    /// 産業大分類コードの URI に API 要求を非同期操作として送信します。(API キーは <see cref="ResasEngine"/> を利用します)
    /// </summary>
    /// <returns>産業大分類コードのコレクション。取得に失敗した場合、空のコレクションを返却します。</returns>
    public static async Task<IEnumerable<IndustriesBroad>> GetIndustriesBroadAsync() => await GetIndustriesBroadAsync(ResasEngine.ApiKey);

    /// <summary>
    /// 産業大分類コードの URI に API 要求を非同期操作として送信します。
    /// </summary>
    /// <param name="key">API キー。</param>
    /// <returns>産業大分類コードのコレクション。取得に失敗した場合、空のコレクションを返却します。</returns>
    public static async Task<IEnumerable<IndustriesBroad>> GetIndustriesBroadAsync(string key)
    {
        string industriesUri = "api/v1/industries/broad";
        string url = _BaseUrl + industriesUri;
        var response = await GetHttpResponce(key, url);

        ApiResult<IndustriesBroad>? result = null;

        if (response != null)
        {
            var options = new JsonSerializerOptions();

            result = JsonSerializer.Deserialize<ApiResult<IndustriesBroad>>(response, options);
        }

        return result?.Result ?? new List<IndustriesBroad>(); ;
    }

    /// <summary>
    /// 指定した RESAS データを表す JSON ファイルを読み込みます。
    /// </summary>
    /// <typeparam name="T">読み込む RESAS データの形式。</typeparam>
    /// <param name="file">ファイルの場所。</param>
    /// <returns>指定した RESAS データのコレクション。取得に失敗した場合、空のコレクションを返却します。</returns>
    public static async Task<IEnumerable<T>> GetLocalAsync<T>(string file) where T : IResas
    {
        using var reader = new StreamReader(file);

        string text = await reader.ReadToEndAsync();

        ApiResult<T>? result = null;

        if (!string.IsNullOrEmpty(text))
        {
            var options = new JsonSerializerOptions();

            result = JsonSerializer.Deserialize<ApiResult<T>>(text, options);
        }

        return result?.Result ?? new List<T>();
    }

    private static async Task<string> GetHttpResponce(string key, string url)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-API-KEY", key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response1 = await client.GetAsync(url);
            var response2 = await response1.Content.ReadAsStringAsync();

            return response2;
        }
    }
}
