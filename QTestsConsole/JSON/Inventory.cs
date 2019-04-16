using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTestsConsole
{
    class Inventory
    {
        public Product[] InventoryItems { get; set; }

        public override string ToString()
        {
            var outText = new StringBuilder();
            foreach(Product prod in InventoryItems)
            {
                outText.AppendLine(prod.ProductName);
            }
            return outText.ToString();
        }
    }
}
