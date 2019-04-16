using QTranser.QTranseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QTranser
{
    partial class AppWindow
    {
        public partial class App : Application
        {
            [STAThread()]
            public static void Main()
            {
                WpfApp1.App app = new WpfApp1.App();
                app.InitializeComponent();
                app.Run();
                // 全局异常捕获
                //new UnhandledException();

            }
        }
    }
}
