using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace QTranser.ViewModles
{
    public class MainViewModel : ViewModel
    {
        private string _strQ = "QTranser";
        public string StrQ
        {
            get => _strQ;
            set
            {
                if (value == _strQ) return;
                _strQ = value;
                OnPropertyChanged();
            }
        }

        private string _strI = "请输入...";
        public string StrI
        {
            get => _strI;
            set
            {
                if (value == _strI) return;
                _strI = value;
                OnPropertyChanged();
            }
        }

        private string _strO = "please input...";
        public string StrO
        {
            get => _strO;
            set
            {
                if (value == _strO) return;
                _strO = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> HistoryWord { get; set; } = new ObservableCollection<string>();

        private string _hotKeyQ;
        public string HotKeyQ
        {
            get => _hotKeyQ + Environment.NewLine + "快捷输入";
            set
            {
                if (value == _hotKeyQ) return;
                _hotKeyQ = value;
                OnPropertyChanged();
            }
        }
        private string _hotKeyW;
        public string HotKeyW
        {
            get => _hotKeyW + Environment.NewLine + "打开/关闭翻译详情界面";
            set
            {
                if (value == _hotKeyW) return;
                _hotKeyW = value;
                OnPropertyChanged();
            }
        }
        private string _hotKeyB;
        public string HotKeyB
        {
            get => _hotKeyB + Environment.NewLine + "一键百度";
            set
            {
                if (value == _hotKeyB) return;
                _hotKeyB = value;
                OnPropertyChanged();
            }
        }
        private string _hotKeyG;
        public string HotKeyG
        {
            get => _hotKeyG + Environment.NewLine + "一键谷歌";
            set
            {
                if (value == _hotKeyG) return;
                _hotKeyG = value; 
                OnPropertyChanged();
            }
        }

        private Brush _logoColor = SystemParameters.WindowGlassBrush;
        public Brush LogoColor
        {
            get => _logoColor;
            set
            {
                if (value == _logoColor) return;
                _logoColor = value;
                OnPropertyChanged();
            }
        }


    }
}
