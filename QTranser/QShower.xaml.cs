using CSDeskBand;
using HotKeyEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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


namespace QTranser
{
    /// <summary>
    /// QShowse.xaml 的交互逻辑
    /// </summary>
    public partial class QShower : Window
    {
        public QShower()
        {
            InitializeComponent();
            DataContext = QTranse.Mvvm;
            this.Height = SystemParameters.WorkArea.Height / 2;
        }

        public void ShowOrHide(double actualHeight, double actualWidth, double pointToScreen)
        {
            try
            {

  
            if (this.IsVisible)
            {
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Deskband.edge == Edge.Top)
                {
                    this.Left = pointToScreen + actualWidth - this.Width;
                    this.Top = actualHeight;
                }
                if (Deskband.edge == Edge.Bottom)
                {
                        
                    this.Left = pointToScreen + actualWidth - this.Width;
                    this.Top = SystemParameters.WorkArea.Height - this.Height;
                }
                if (Deskband.edge == Edge.Left)
                {
                    this.Left = actualWidth;
                    this.Top = SystemParameters.WorkArea.Height - this.Height;
                }
                if (Deskband.edge == Edge.Right)
                {
                    this.Left = SystemParameters.WorkArea.Width - this.Width;
                    this.Top = SystemParameters.WorkArea.Height - this.Height;
                }
                this.Visibility = Visibility.Visible;
                this.Topmost = true;
                this.Activate();
                this.StrIBox.Focus();
                this.StrIBox.SelectionStart = this.StrIBox.Text.Length;
            }

            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        public void inputStrProsessing(object sender, KeyEventArgs e)
        {
            string str = ((TextBox)sender).Text;
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.L)
            {
                ((TextBox)sender).Clear();
            }
            if (e.Key == Key.Enter)
            {
                if (str.EndsWith("/") || str.EndsWith("?"))
                {
                    Process.Start("https://www.baidu.com/s?ie=UTF-8&wd=" + str);
                }
                else
                {
                    Clipboard.SetText(str);
                }
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private bool IsTop { get; set; } = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //后台代码动态修改控件模板属性 参考链接：https://www.itsvse.com/thread-2740-1-1.html
            Border border = (Border)((Button)sender).Template.FindName("Border", (Button)sender);
            TextBlock textBlock = (TextBlock)((Button)sender).Template.FindName("topPrompt", (Button)sender);
            if (IsTop)
            {
                border.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
                this.Deactivated -= Window_Deactivated;
                textBlock.Text = " 取消置顶";
                IsTop = false;

            }
            else
            {
                border.Background = Brushes.Transparent;
                this.Deactivated += Window_Deactivated;
                textBlock.Text = " 置顶";
                IsTop = true;
            }
          
        }

        private void StrIBox_KeyUp(object sender, KeyEventArgs e)
        {
            inputStrProsessing(sender, e);
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(((TextBlock)sender).Text);
        }

        private void TextBlock_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            QTranse.Mvvm.HistoryWord.RemoveAt(HistoryList.SelectedIndex);
        }


    }
}
