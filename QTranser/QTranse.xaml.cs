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
using System.Text.RegularExpressions;

namespace QTranser
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class QTranse : UserControl
    {
        public static Edge edge { get; set; }
        public static MainViewModel Mvvm { get; set; } = new MainViewModel();
        public static HotKeyManager HotKeyManage;

        private ClipboardMonitor _clipboardMonitor;
        private InputSimulator _sim { get; set; } = new InputSimulator();
        private QShower _shower { get; set; } = new QShower();
        private ForegroundWindow _foregroundWindow { get; set; } = new ForegroundWindow();

        public QTranse()
        {
            InitializeComponent();
            DataContext = Mvvm;
        }

        private void QTranser_Loaded(object sender, RoutedEventArgs e)
        {
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
            string str = ClipboardGetText();
            if (str == "") return;
            str = AddSpacesBeforeCapitalLetters(str);

            Mvvm.StrQ = "...";
            Mvvm.StrI = "...";
            TranslationResultDisplay(str);

            bool isRepeat = Mvvm.HistoryWord.Any<HistoryWord>(o => o.Word.Trim().ToLower() == str.Trim().ToLower());
            if(isRepeat) return;
            else
            {
                Mvvm.HistoryWord.Insert(0, new HistoryWord() { Word = str });
            }

            if (Mvvm.HistoryWord.Count > 12) Mvvm.HistoryWord.RemoveAt(12);
        }
        private string ClipboardGetText()
        {
            var str = "";
            if (Clipboard.ContainsText())
            {
                try { str = Clipboard.GetText(); }
                catch (Exception err)
                { Loger.str(err.ToString(), true); }
            }
            return str.Trim().Replace("  ", "");
        }
        private string AddSpacesBeforeCapitalLetters(string str)
        {
            str = Regex.Replace(str, "([a-z])([A-Z](?=[A-Z])[a-z]*)", "$1 $2");
            str = Regex.Replace(str, "([A-Z])([A-Z][a-z])", "$1 $2");
            str = Regex.Replace(str, "([a-z])([A-Z][a-z])", "$1 $2");
            str = Regex.Replace(str, "([a-z])([A-Z][a-z])", "$1 $2");
            return str;
        }
        private async void TranslationResultDisplay(string str)
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

                    string detailsStr = transResult?.translation?[0] + Environment.NewLine;

                    if (transResult?.basic != null)
                    {
                        detailsStr += "----------------" + Environment.NewLine;
                        foreach (var strr in transResult?.basic?.explains)
                        {
                            detailsStr += strr + Environment.NewLine;
                        }
                    }

                    if (transResult?.web != null)
                    {
                        detailsStr += "----------------" + Environment.NewLine;
                        foreach (var element in transResult.web)
                        {
                            detailsStr += element.key + Environment.NewLine;
                            if (element?.value != null)
                            {
                                foreach (var strr in element.value)
                                {
                                    detailsStr += "  " + strr + Environment.NewLine;
                                }
                            }
                        }
                    }
                    string s = transResult?.translation?[0];
                    Mvvm.StrQ = s.Replace("\n"," ");
                    Mvvm.StrO = detailsStr.Substring(0, detailsStr.Length - 2);
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
                _shower.Activate();
                _shower.StrIBox.Focus();
                _shower.StrIBox.SelectionStart = _shower.StrIBox.Text.Length;
               
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

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            inputStrProsessing(sender, e);
        }

        public static void inputStrProsessing(object sender, KeyEventArgs e)
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
    }
}