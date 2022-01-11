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

public class ResasClient
{
    private string _BaseUrl = "https://opendata.resas-portal.go.jp/";

    public async Task<IEnumerable<Prefecture>> GetPrefecturesAsync() => await GetPrefecturesAsync(ResasEngine.ApiKey);

    public async Task<IEnumerable<Prefecture>> GetPrefecturesAsync(string key)
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

    public async Task<IEnumerable<T>> GetLocalAsync<T>(string file)
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

    public async Task<IEnumerable<IndustriesBroad>> GetIndustriesBroadAsync() => await GetIndustriesBroadAsync(ResasEngine.ApiKey);

    public async Task<IEnumerable<IndustriesBroad>> GetIndustriesBroadAsync(string key)
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

    private async Task<string> GetHttpResponce(string key, string url)
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
