using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTranserUninstall
{
    class Program
    {
        static void Main(string[] args)
        {
            Process proc = new Process();
            try
            {
                string targetDir = string.Format(@".\tools");  // this is where installer.bat lies
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = "UninstallEXE.bat";
                //proc.StartInfo.Arguments = string.Format("10");//this is argument
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;  // 这里设置DOS窗口不显示，经实践可行
                proc.Start();
                proc.WaitForExit();
                RestartExplorer.Restart();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }
    }
}
