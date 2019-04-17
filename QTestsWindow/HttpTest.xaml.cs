using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QTestsWindow
{
    /// <summary>
    /// HttpTest.xaml 的交互逻辑
    /// </summary>
    public partial class HttpTest : Window
    {
        public HttpTest()
        {
            InitializeComponent();
        }
        private const string NorthwindUrl = "http://services.odata.org/Northwind/Northwind.svc/Regions";
        public async Task GetDataSimpleAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(NorthwindUrl);
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine((int)res.StatusCode);
                    Console.WriteLine(":::::::::::");
                    Console.WriteLine(res.ReasonPhrase);
                    string resBodyAsText = await res.Content.ReadAsStringAsync();
                    Console.WriteLine(res.ReasonPhrase);
                }
            }
        }
    }
}
