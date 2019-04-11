using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace QTranser.QTranseLib
{
    /// <summary>
    /// Clipboard Monitor class to notify if the clipboard content changes
    /// </summary>
    public class SysColorChanger
    {
        private const int WM_SYSCOLORCHANGE = 0015;

        private IntPtr Handle;

        /// <summary>
        /// Event for clipboard update notification.
        /// </summary>
        public event Action SysColorChange;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj">window or UserControl of the application.</param>
        /// <param name="start">Enable clipboard notification on startup or not.</param>
        public SysColorChanger(Object obj)
        {
            if (obj is Window)
            { Handle = new WindowInteropHelper((Window)obj).EnsureHandle(); }
            if (obj is UserControl)
            { Handle = ((HwndSource)PresentationSource.FromVisual((UserControl)obj)).Handle; }

            HwndSource.FromHwnd(Handle)?.AddHook(HwndHandler);
        }

        private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == WM_SYSCOLORCHANGE)
            {
                this.SysColorChange?.Invoke();
            }
            handled = false;
            return IntPtr.Zero;
        }

    }
}
