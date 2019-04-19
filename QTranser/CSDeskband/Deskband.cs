using System;
using System.Runtime.InteropServices;
using System.Windows;
using CSDeskBand;
using QTranser.QTranseLib;

namespace QTranser
{
    [ComVisible(true)]
    [Guid("AA01ACB3-6CCC-497C-9CE6-9211F2EDFC10")]
    //[CSDeskBandRegistration(Name = "Qtranser")]
    [CSDeskBandRegistration(Name = "Qtranser",ShowDeskBand = true)]
    public class Deskband : CSDeskBandWpf
    {
        public static Edge Edger { get; set; }

        public Deskband()
        {
            Options.MinHorizontalSize.Width = 170;
            Edger = TaskbarInfo.Edge;
            TaskbarInfo.TaskbarEdgeChanged += (sender, e) => Edger = e.Edge;
            // 全局异常捕获
            new GlobalUnhandledException();
        }

        protected override UIElement UIElement => new QTranse();

        protected override void DeskbandOnClosed()
        {
            QTranse.HotKeyManage.Dispose();
            QTranse.Shower.Close();
            Properties.Settings.Default.Save();
        }
    }
}
