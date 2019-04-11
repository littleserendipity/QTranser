using System;
using System.Collections.Generic;
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
