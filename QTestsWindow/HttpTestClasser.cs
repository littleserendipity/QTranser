using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace QTestsWindow
{
    public class HttpTestClasser
    {

        
        //private const string idkey = "https://raw.githubusercontent.com/xyfll7/Topology/master/IDKey/idkey.json";
        private const string NorthwindUrl = "http://services.odata.org/Northwind/Northwind.svc/Regions";
        private const string NorthwindUrl2 = "http://http2.akamai.com/demo";
        private const string IncorrectUrl = "http://services.odata.org/Northwind1/Northwind.svc/Regions";
        //private const string NorthwindUrl = "https://gss3.bdstatic.com/-Po3dSag_xI4khGkpoWK1HF6hhy/baike/w%3D268%3Bg%3D0/sign=b6e552389bef76c6d0d2fc2da52d9ac7/2f738bd4b31c8701f2b332ff2c7f9e2f0708ffa6.jpg";

        public async Task GetDataSimpleAsync()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(NorthwindUrl);
                if (res.IsSuccessStatusCode)
                {
                    WriteLine((int)res.StatusCode);
                    WriteLine(res.ReasonPhrase);
                    string resBodyAsText = await res.Content.ReadAsStringAsync();
                    WriteLine(resBodyAsText.Length);
                    WriteLine(":::::::::::");
                    WriteLine(resBodyAsText);
                }
            }
        }
        public async Task GetDataAdvancedAsync()
        {
            using (var client = new HttpClient())
            {
                var req0 = new HttpRequestMessage(HttpMethod.Get, NorthwindUrl);
                var req1 = new HttpRequestMessage(HttpMethod.Put, NorthwindUrl);
                var req2 = new HttpRequestMessage(HttpMethod.Post, NorthwindUrl);
                var req3 = new HttpRequestMessage(HttpMethod.Delete, NorthwindUrl);

                var req4 = new HttpRequestMessage(HttpMethod.Trace, NorthwindUrl);
                var req5 = new HttpRequestMessage(HttpMethod.Options, NorthwindUrl);
                var req6 = new HttpRequestMessage(HttpMethod.Head, NorthwindUrl);
                var res0 = await client.SendAsync(req0);
                //var res1 = await client.PutAsync(NorthwindUrl,content);
                var res2 = await client.SendAsync(req0);
                var res3 = await client.SendAsync(req0);
                if (res0.IsSuccessStatusCode)
                {
                    WriteLine((int)res0.StatusCode);
                    WriteLine(res0.ReasonPhrase);
                    string resBobyAsText = await res0.Content.ReadAsStringAsync();
                    WriteLine(resBobyAsText.Length);
                    WriteLine(":::::::::::::::::");
                    WriteLine(resBobyAsText);
                }
            }
        }
        public async Task GetDataWithExceptionsAsync()
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    ShowHeaders(client.DefaultRequestHeaders);
                    var res = await client.GetAsync(IncorrectUrl); // 默认不会抛出异常
                    res.EnsureSuccessStatusCode(); // 检查IsSuccessStatusCode如果是false  就会抛出异常
                    ShowHeaders(res.Headers);

                    WriteLine("111111111111");
                    WriteLine(res.StatusCode);
                    WriteLine(res.ReasonPhrase);
                    string responseBodyAsText = await res.Content.ReadAsStringAsync();
                    WriteLine(responseBodyAsText.Length);
                    WriteLine("22222222222222");
                    WriteLine(responseBodyAsText);
                }
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
        public async Task GetDataWithHeadersAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    var res0 = await client.GetAsync(NorthwindUrl);
                    var res1 = await client.GetStringAsync(NorthwindUrl);
                    ShowHeaders(client.DefaultRequestHeaders);

                    WriteLine((int)res0.StatusCode);
                    WriteLine(res0.ReasonPhrase);
                    var resString = await res0.Content.ReadAsStringAsync();
                    var resByteArray = await res0.Content.ReadAsByteArrayAsync();
                    var resStream = await res0.Content.ReadAsStreamAsync();
                    WriteLine(resString.Length);
                    WriteLine("!!!!!!!!!!!!");
                    WriteLine(resString);
                    ShowHeaders(res0.Headers);
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        public void ShowHeaders(HttpHeaders headers)
        {
            WriteLine("::::::::::::::::");
            foreach(var header in headers)
            {
                WriteLine($"Header: {header.Key}");
                WriteLine($"Value: {string.Join("", header.Value)}");
            }
            WriteLine("::::::::::::::::~~");
        }

        public async Task GetDataWithMessageHandlerAsync()
        {
            try
            {
                using (var client = new HttpClient(new SampleMessageHandler("errors")))
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    ShowHeaders(client.DefaultRequestHeaders);

                    HttpResponseMessage response = await client.GetAsync(NorthwindUrl);
                    response.EnsureSuccessStatusCode();

                    ShowHeaders(response.Headers);

                    WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);

                }
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.Message}");
            }
        }

        private const string idkey = "https://raw.githubusercontent.com/xyfll7/Topology/master/IDKey/idkey.json";
        public async Task getidkey()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, idkey);
                    HttpResponseMessage res = await client.SendAsync(request);
                    res.EnsureSuccessStatusCode(); // 检查IsSuccessStatusCode如果是false  就会抛出异常
                    var result = await res.Content.ReadAsStringAsync();
                    dynamic transResult = JToken.Parse(result) as dynamic;
                    MessageBox.Show(transResult?.ID.ToString());
                    MessageBox.Show(transResult?.Key.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
