using System;
using System.Runtime.InteropServices;

namespace QTranser.QTranseLib
{
    public class ForegroundWindow
    {
        public void SetForeground(string windowName)
        {
            IntPtr Handle = FindWindow(windowName, null); //The className & WindowName I got using Spy++
            if (Handle != null)
            {
                bool topMost = SetForegroundWindow(Handle);
            }
        }
        //// 函数讲解：  https://blog.csdn.net/qq_34126663/article/details/70255257
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
