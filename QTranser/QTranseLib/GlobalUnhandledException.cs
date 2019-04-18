using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QTranser.QTranseLib
{
    public class GlobalUnhandledException: Application
    {
        public GlobalUnhandledException()
        {
            // UI线程未捕获异常处理事件
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            // Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            // 非UI线程未捕获异常处理事件(包含UI线程异常）
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                MessageBox.Show("##捕获未处理异常:" + e.Exception.Message);
                Loger.str("TargetSite::" + e.Exception.TargetSite,true);
                Loger.str("StackTrace::" + e.Exception.StackTrace,true);
                Loger.str("InnerException::" + e.Exception.InnerException,true);
                Loger.str("HResult::" + e.Exception.HResult,true);
                Loger.str("HelpLink::" + e.Exception.HelpLink,true);
                Loger.str("Data::" + e.Exception.Data,true);
                Loger.str(":::::::::::::::::::::::::::::::::::::::！！",true);
            }
            catch (Exception ex)
            {
                //此时程序出现严重异常，将强制结束退出
                MessageBox.Show("程序发生致命错误，将终止，请联系运营商！");
            }
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show("//捕获线程内未处理异常：" + e.Exception.Message);
            e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("!!程序发生致命错误，将终止，请联系运营商！\n");
            }
            sbEx.Append("!!捕获未处理异常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).Message);
                sbEx.Append(((Exception)e.ExceptionObject));
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }
            MessageBox.Show(sbEx.ToString());
            Loger.str(sbEx.ToString());
        }
    }
}
