using System;
using System.Collections.Generic;
using System.Text;

namespace Fridge
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public double Quantity { get; set; }

        public InventoryItem(string name, double quantity)
        {
            Name = name;
            Quantity = quantity;
        }


        public InventoryItem()
        {
        }
    }
}
