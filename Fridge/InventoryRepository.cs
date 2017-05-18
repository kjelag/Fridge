using System;
using System.Collections.Generic;
using System.Text;

namespace Fridge
{
    class InventoryRepository : IInventoryRepository

    {
        private static List<InventoryItem> inventoryItemList = new List<InventoryItem>();

        public List<InventoryItem> List()
        {
            return inventoryItemList;
        }
        public void AddInventoryItem(InventoryItem inventoryItem)
        {
            inventoryItemList.Add(inventoryItem);
        }

        public InventoryItem Get(string name)
        {
            return inventoryItemList.Find(inventory => inventory.Name == name);
        }
        
        public void UpdateInventoryItem(InventoryItem inventoryItem)
        {
            InventoryItem existingInventoryItem = inventoryItemList.Find(inventory => inventory.Name == inventoryItem.Name);
            inventoryItemList.Remove(existingInventoryItem);
            inventoryItemList.Add(inventoryItem);
        }
    }
}
