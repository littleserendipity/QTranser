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
        HttpTestClasser http { get; set; } = new HttpTestClasser();
        public HttpTest()
        {
            InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //await http.GetDataAdvancedAsync();
            //await http.GetDataWithExceptionsAsync();
            //await http.GetDataWithHeadersAsync() ;
            //await http.GetDataWithMessageHandlerAsync();
            await http.getidkey();
        }


        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(HttpTest), new PropertyMetadata(string.Empty));

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Url);
        }
    }
}
