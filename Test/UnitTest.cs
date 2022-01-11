using NUnit.Framework;
using RESAS.Services;
using RESAS.Services.Data;
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

namespace Test;

public class Tests
{
    private string ApiKey { get; set; } = "";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // var api = Environment.GetEnvironmentVariable("RESAS_API", EnvironmentVariableTarget.User);
        DotNetEnv.Env.Load(".env");
        var key = DotNetEnv.Env.GetString("RESAS_API");

        ApiKey = key;
        ResasEngine.ApiKey = key;
    }

    [Test]
    public async Task RESAS_都道府県コード()
    {
        var client = new ResasClient();

        var prefectures1 = await client.GetPrefecturesAsync(ApiKey);
        var prefectures2 = await client.GetPrefecturesAsync();


        Assert.AreEqual(prefectures1.Count(), 47);
        Assert.AreEqual(prefectures2.Count(), 47);
        Assert.IsTrue(IsSame(prefectures1.Select(p => (p.Name, p.Code)), prefectures2.Select(p => (p.Name, p.Code))));
    }

    [Test]
    public async Task RESAS_産業大分類コード()
    {
        var client = new ResasClient();

        var industriesBroads1 = await client.GetIndustriesBroadAsync(ApiKey);
        var industriesBroads2 = await client.GetIndustriesBroadAsync();

        Assert.AreEqual(industriesBroads1.Count(), 20);
        Assert.AreEqual(industriesBroads2.Count(), 20);
        Assert.IsTrue(IsSame(industriesBroads1.Select(p => (p.Name, p.Code)), industriesBroads2.Select(p => (p.Name, p.Code))));
    }

    [TestCase(@"Memento/Prefecture.json", 47)]
    [TestCase(@"Memento/IndustriesBroad.json", 20)]
    public async Task RESAS_ローカルファイル(string file, int count)
    {
        var client = new ResasClient();

        var data = await client.GetLocalAsync<Prefecture>(file);

        Assert.AreEqual(data.Count(), count);
    }

    private bool IsSame<T1, T2>(IEnumerable<(T1, T2)> data1, IEnumerable<(T1, T2)> data2) 
        where T1 : IComparable
        where T2 : IComparable
    {
        if (data1.Count() != data2.Count()) return false;

        bool result = true;
        var d1 = data1.ToArray();
        var d2 = data2.ToArray();

        for (int i = 0; i < data1.Count(); i++)
        {
            if (d1[i].Item1.CompareTo(d2[i].Item1) != 0)
            {
                result = false;
                break;
            }

            if (d1[i].Item2.CompareTo(d2[i].Item2) != 0)
            {
                result = false;
                break;
            }
        }

        return result;
    }
}