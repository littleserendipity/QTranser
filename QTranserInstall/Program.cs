using CSDeskBand;
using QTranser;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace QTranserInstall
{
    class Program
    {
        static void Main(string[] args)
        {
            BandOperate.HideBand(typeof(QTranse));
            if (MessageBox.Show($"::打开QTranser::点确定{Environment.NewLine}{Environment.NewLine}::关闭QTranser::点取消", "打开/关闭QTranser", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Process proc = new Process();
                string targetDir = string.Format(@".\tools");  // this is where installer.bat lies
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = "UninstallEXE.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;  // 这里设置DOS窗口不显示，经实践可行
                proc.Start();
                Thread.Sleep(3000);
                RestartExplorer.Restart();
                proc.StartInfo.FileName = "installerEXE.bat";
                proc.Start();
                Thread.Sleep(3000);
                BandOperate.ShowBand(typeof(QTranse));
            }
        }
    }
}
