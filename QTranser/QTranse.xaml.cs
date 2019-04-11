using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QTranser.ViewModles;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QTranser.QTranseLib;
using GlobalHotKey;
using WindowsInput;
using WindowsInput.Native;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CSDeskBand;
using System.Net;

namespace QTranser
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class QTranse : UserControl
    {
        public QTranse()
        {
            InitializeComponent();
        }

        public static Edge edge { get; set; }
        public static MainViewModel Mvvm { get; set; } = new MainViewModel();
        private ClipboardMonitor _clipboardMonitor;
        private HotKeyManager _hotKeyManager;
        private InputSimulator _sim { get; set; } = new InputSimulator();
        private QShower Shower { get; set; } = new QShower();
        private ForegroundWindow _foregroundWindow { get; set; } = new ForegroundWindow();

        private void QTranser_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Mvvm;
            _clipboardMonitor = new ClipboardMonitor(this);
            _clipboardMonitor.ClipboardUpdate += OnClipboardUpdate;

            _hotKeyManager = new HotKeyManager(this);
            _hotKeyManager.KeyPressed += OnHotKeyPressed;
            RegisterHotKey();

            var SysColor = new SysColorChanger(this);
            SysColor.SysColorChange += () => Mvvm.LogoColor = SystemParameters.WindowGlassBrush;

            ShowQShower_MouseLeftButtonDown(null, null);
        }

        private void RegisterHotKey()
        {
            var hotKeyQ = _hotKeyManager.Register(Key.Q, ModifierKeys.Control);
            var hotKeyW = _hotKeyManager.Register(Key.W, ModifierKeys.Control);
            var hotKeyB = _hotKeyManager.Register(Key.B, ModifierKeys.Control);
        }

        private void OnHotKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.HotKey.Key == Key.Q)
            {
                _foregroundWindow.SetForeground("Shell_TrayWnd");
                textBox.Focus();
                textBox.Clear();
            }
            if(e.HotKey.Key == Key.W)
            {
                ShowQShower_MouseLeftButtonDown(null, null);
            }
            if (e.HotKey.Key == Key.B)
            {
                _sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);

            }
        }


        private string ClipboardGetText()
        {
            var str = "";
            if (Clipboard.ContainsText())
            {
                try { str = Clipboard.GetText(); }
                catch (Exception err)
                { MessageBox.Show(err.ToString()); }
            }
            return str;
        }

        private void OnClipboardUpdate(object sender, EventArgs e)
        {
            Mvvm.StrQ = "...";
            Mvvm.StrI = "...";
            try
            {
                string str = ClipboardGetText();

                var translator = new Translator();
                string transResultJson = translator.dao(str);
                dynamic transResult = JToken.Parse(transResultJson) as dynamic;
                Mvvm.StrI = str;
                // 将翻译结果写入 transResult.json 文件
                Loger.json(transResult);

                string resultStr = transResult?.translation?[0] + Environment.NewLine;

                if (transResult?.basic != null)
                {
                    resultStr += "----------------" + Environment.NewLine;
                    foreach (var strr in transResult?.basic?.explains)
                    {
                        resultStr += strr + Environment.NewLine;
                    }
                }

                if (transResult?.web != null)
                {
                    resultStr += "----------------" + Environment.NewLine;
                    foreach (var element in transResult.web)
                    {
                        resultStr += element.key + Environment.NewLine;
                        if (element?.value != null)
                        {
                            foreach (var strr in element.value)
                            {
                                resultStr += "  " + strr + Environment.NewLine;
                            }
                        }
                    }
                }
                Mvvm.StrQ = transResult?.translation?[0];
                Mvvm.StrO = resultStr.Substring(0, resultStr.Length - 2); 
            }
            catch (WebException err)
            {
                Mvvm.StrQ = "哎呦~无法连接网络...";
                Mvvm.StrI = "哎呦~无法连接网络...";
                Mvvm.StrO = "Ouch ~ can't connect to Internet...";
            }
            catch (Exception err)
            {
                Loger.str(err.ToString());
            }

        }

        private void ShowQShower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Shower.IsVisible)
            {
                Shower.Visibility = Visibility.Hidden;
            }
            else
            {
                if (edge == Edge.Top)
                {
                    Shower.Left = PointToScreen(new Point()).X + this.ActualWidth - Shower.Width;
                    Shower.Top = this.ActualHeight;
                }
                if (edge == Edge.Bottom)
                {
                    Shower.Left = PointToScreen(new Point()).X + this.ActualWidth - Shower.Width;
                    Shower.Top = SystemParameters.WorkArea.Height - Shower.Height;
                }
                if (edge == Edge.Left)
                {
                    Shower.Left = this.ActualWidth;
                    Shower.Top = SystemParameters.WorkArea.Height - Shower.Height;
                }
                if (edge == Edge.Right)
                {
                    Shower.Left = SystemParameters.WorkArea.Width - Shower.Width;
                    Shower.Top = SystemParameters.WorkArea.Height - Shower.Height;
                }
                Shower.Visibility = Visibility.Visible;
                Shower.Topmost = true;
            }
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Shower.Deactivated -= Shower.Window_Deactivated;
            // 必须借助真实鼠标/键盘按键 SetForeground函数 才能抢到焦点。
            _sim.Mouse.RightButtonUp();

            _foregroundWindow.SetForeground("Shell_TrayWnd");

            textBox.Focus();
            textBox.SelectionStart = textBox.Text.Length;
        }
        
        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Shower.Deactivated += Shower.Window_Deactivated;
        }
    }
}
