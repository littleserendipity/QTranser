
using Newtonsoft.Json.Linq;
using QTranserUninstall;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;
namespace QTests
{
    class QTranserInstaller
    {
        static void Mainer()
        {
            getidkey();
            ReadLine();
        }
        private const string idkey = "https://api.github.com/users/xyfll7/orgs";
        public static async Task getidkey()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                    var request = new HttpRequestMessage(HttpMethod.Get, idkey);
                    var res = await client.SendAsync(request); // 默认不会抛出异常
                    //var res = await client.SendAsync(request);
                    //res.EnsureSuccessStatusCode(); // 检查IsSuccessStatusCode如果是false  就会抛出异常
                    var result = await res.Content.ReadAsStringAsync();
                    //dynamic transResult = JToken.Parse(result) as dynamic;
                    WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
