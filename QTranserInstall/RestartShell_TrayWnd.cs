using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace QTranserInstall
{
    class RestartExplorer
    {
        static public void Restart()
        {
            string explorer = string.Format($"{ Environment.GetEnvironmentVariable("WINDIR")}\\explorer.exe");
            Process process = new Process();
            process.StartInfo.FileName = explorer;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }
    }
}
