using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CSDeskBand;
using CSDeskBand.ContextMenu;

namespace QTranser
{
    [ComVisible(true)]
    [Guid("AA01ACB3-6CCC-497C-9CE6-9211F2EDFC10")]
    [CSDeskBandRegistration(Name = "Qtranser")]
    public class Deskband : CSDeskBandWpf
    {
        public Deskband()
        {
            Options.ContextMenuItems = ContextMenuItems;
            Options.MinHorizontalSize.Width = 170;
            QTranse.edge = TaskbarInfo.Edge;
            TaskbarInfo.TaskbarEdgeChanged += (sender, e) => QTranse.edge = e.Edge;
        }

        protected override UIElement UIElement => new QTranse();

        private List<DeskBandMenuItem> ContextMenuItems
        {
            get
            {
                var action = new DeskBandMenuAction("Action - Toggle submenu");
                var separator = new DeskBandMenuSeparator();
                var submenuAction = new DeskBandMenuAction("Submenu Action - Toggle checkmark");
                var submenu = new DeskBandMenu("Submenu")
                {
                    Items = { submenuAction }
                };

                action.Clicked += (sender, args) => submenu.Enabled = !submenu.Enabled;
                submenuAction.Clicked += (sender, args) => submenuAction.Checked = !submenuAction.Checked;

                return new List<DeskBandMenuItem>() { action, separator, submenu };
            }
        }
    }
}
