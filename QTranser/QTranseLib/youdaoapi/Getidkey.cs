using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QTranser.QTranseLib
{
    public static class Getidkey
    {
        private const string idkey = "https://raw.githubusercontent.com/xyfll7/Topology/master/IDKey/idkey.json";
        public static async Task getidkey()
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
                    Idkey.Id = transResult?.ID.ToString();
                    Idkey.Key = transResult?.Key.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
