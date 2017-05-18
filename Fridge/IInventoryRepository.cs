using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Fridge
{
    public interface IInventoryRepository
    {
        List<InventoryItem> List();
        InventoryItem Get(string name);
        void AddInventoryItem(InventoryItem inventoryItem);
        void UpdateInventoryItem(InventoryItem inventoryItem);
    } 
}
