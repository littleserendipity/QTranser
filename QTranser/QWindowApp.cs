using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QTranser
{
    class QWindowApp
    {
        public partial class App : Application
        {
            [STAThread()]
            public static void Main()
            {
                Application app = new Application();
                QWindow qWindow = new QWindow();
                app.Run(qWindow);
            }
        }
    }
}
