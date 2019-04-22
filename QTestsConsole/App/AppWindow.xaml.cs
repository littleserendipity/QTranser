using System.Windows;

namespace QTranser
{
    /// <summary>
    /// QWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();

            new QTranse();
        }
    }
}
