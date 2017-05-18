using System;
using System.Collections.Generic;
using System.Text;
using Fridge;

namespace FridgeUnitTest
{
    public class FakeInventoryRepository : IInventoryRepository
        
    {
        public List<InventoryItem> InventoryItems;

        public FakeInventoryRepository()
        {
            InventoryItems = new List<InventoryItem>();
        }

        public List<InventoryItem> List()
        {
            return InventoryItems;
        }

        public InventoryItem Get(string name)
        {
            return InventoryItems.Find(inventoryItem => inventoryItem.Name == name);
        }

        public void AddInventoryItem(InventoryItem inventoryItem)
        {
            InventoryItems.Add(inventoryItem);
        }

        public void UpdateInventoryItem(InventoryItem inventoryItem)
        {
            var existingInventoryItem = InventoryItems.Find(item => item.Name == inventoryItem.Name);
            InventoryItems.Remove(inventoryItem);
            InventoryItems.Add(inventoryItem);
        }
    }
}
