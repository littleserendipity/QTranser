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
using System.Threading;
using System.Collections.ObjectModel;

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
        public static HotKeyManager HotKeyManage;

        private ClipboardMonitor _clipboardMonitor;
        private InputSimulator _sim { get; set; } = new InputSimulator();
        private QShower _shower { get; set; } = new QShower();
        private ForegroundWindow _foregroundWindow { get; set; } = new ForegroundWindow();

       

        private void QTranser_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Mvvm;
            _clipboardMonitor = new ClipboardMonitor(this);
            _clipboardMonitor.ClipboardUpdate += OnClipboardUpdate;

            HotKeyManage = new HotKeyManager(this);
            HotKeyManage.KeyPressed += OnHotKeyPressed;
            RegisterHotKey();

            var SysColor = new SysColorChanger(this);
            SysColor.SysColorChange += () => Mvvm.LogoColor = SystemParameters.WindowGlassBrush;

            ShowQShower_MouseLeftButtonDown(null, null);

          
        }

        private void OnClipboardUpdate(object sender, EventArgs e)
        {
            Mvvm.StrQ = "...";
            Mvvm.StrI = "...";
            TansResultProcessing(ClipboardGetText());
            try
            {
                Mvvm.HistoryWord.Insert(0, ClipboardGetText());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
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
            return str.Trim().Replace("  ", "");
        }
        private async void TansResultProcessing(string str)
        {
            try
            {

                await Task.Run(() =>
                {
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
                });
            }
            catch (WebException)
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
        
        private void RegisterHotKey()
        {
            try
            {
                var hotKeyQ = HotKeyManage.Register(Key.Q, ModifierKeys.Control);
                Mvvm.HotKeyQ = HotKeyManage.ToString();
                HotKeyEditor.HotKey.hotKeyModQ = ModifierKeys.Control;
                HotKeyEditor.HotKey.hotKeyQ = Key.Q;

            }
            catch
            {
                Mvvm.HotKeyQ = HotKeyManage.ToString() + "(冲突)";
            }
            try
            {
                var hotKeyW = HotKeyManage.Register(Key.W, ModifierKeys.Control);
                Mvvm.HotKeyW = HotKeyManage.ToString();
                HotKeyEditor.HotKey.hotKeyModW = ModifierKeys.Control;
                HotKeyEditor.HotKey.hotKeyW = Key.W;
            }
            catch
            {
                Mvvm.HotKeyW = HotKeyManage.ToString() + "(冲突)";
            }
            try
            {
                var hotKeyB = HotKeyManage.Register(Key.B, ModifierKeys.Control);
                Mvvm.HotKeyB = HotKeyManage.ToString();
                HotKeyEditor.HotKey.hotKeyModB = ModifierKeys.Control;
                HotKeyEditor.HotKey.hotKeyB = Key.B;
            }
            catch
            {
                Mvvm.HotKeyB = HotKeyManage.ToString() + "(冲突)";
            }
            try
            {
                var hotKeyG = HotKeyManage.Register(Key.G, ModifierKeys.Control);
                Mvvm.HotKeyG = HotKeyManage.ToString();
                HotKeyEditor.HotKey.hotKeyModG = ModifierKeys.Control;
                HotKeyEditor.HotKey.hotKeyG = Key.G;
            }
            catch
            {
                Mvvm.HotKeyG = HotKeyManage.ToString() + "(冲突)";
            }
        }

        private void OnHotKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.HotKey.Key == HotKeyEditor.HotKey.hotKeyQ)
            {
                _foregroundWindow.SetForeground("Shell_TrayWnd");
                textBox.Focus();
                textBox.Clear();
            }
            if(e.HotKey.Key == HotKeyEditor.HotKey.hotKeyW)
            {
                ShowQShower_MouseLeftButtonDown(null, null);
            }
            if (e.HotKey.Key == HotKeyEditor.HotKey.hotKeyB)
            {
                _sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
                Thread.Sleep(20);
                string str = ClipboardGetText();
                Process.Start("https://www.baidu.com/s?ie=UTF-8&wd=" + str);
            }
            if (e.HotKey.Key == HotKeyEditor.HotKey.hotKeyG)
            {
                _sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
                Thread.Sleep(20);
                string str = ClipboardGetText();
                Process.Start("http://google.com#q=" + str);
            }
        }
      



        private void ShowQShower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_shower.IsVisible )
            {
                _shower.Visibility = Visibility.Hidden;
            }
            else
            {
               
                if (edge == Edge.Top)
                {
                    _shower.Left = PointToScreen(new Point()).X + this.ActualWidth - _shower.Width;
                    _shower.Top = this.ActualHeight;
                }
                if (edge == Edge.Bottom)
                {
                    _shower.Left = PointToScreen(new Point()).X + this.ActualWidth - _shower.Width;
                    _shower.Top = SystemParameters.WorkArea.Height - _shower.Height;
                }
                if (edge == Edge.Left)
                {
                    _shower.Left = this.ActualWidth;
                    _shower.Top = SystemParameters.WorkArea.Height - _shower.Height;
                }
                if (edge == Edge.Right)
                {
                    _shower.Left = SystemParameters.WorkArea.Width - _shower.Width;
                    _shower.Top = SystemParameters.WorkArea.Height - _shower.Height;
                }
                _shower.Visibility = Visibility.Visible;
                _shower.Topmost = true;
               
            }
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            // 必须借助真实鼠标/键盘按键 SetForeground函数 才能抢到焦点。
            _sim.Mouse.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
            _foregroundWindow.SetForeground("Shell_TrayWnd");
            textBox.Focus();
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}
