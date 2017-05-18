using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fridge
{
    public class FridgeService
    {
        private readonly IInventoryRepository _inventoryRepository;
        
        public FridgeService()
        {
          _inventoryRepository = new InventoryRepository();  
        }

        public FridgeService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        //public FridgeService()
        public bool IsItemAvailable(string name, double quantity)
        {
            var inventoryItem = _inventoryRepository.Get(name);
            if (inventoryItem == null) return false;
            return inventoryItem.Quantity >= quantity;
        }


        public List<InventoryItem> GetAllInventoryItems()
        {
            return _inventoryRepository.List();
        }

        public InventoryItem GetInventoryItem(string name)
        {
            return _inventoryRepository.Get(name);
        }

        public void AddIngredientToFridge(InventoryItem inventoryItem)
        {
            var existingInventoryItem = _inventoryRepository.Get(inventoryItem.Name);

            if (existingInventoryItem == null)
            {
                _inventoryRepository.AddInventoryItem(inventoryItem);
                return;
            }

            inventoryItem.Quantity += inventoryItem.Quantity;
            _inventoryRepository.UpdateInventoryItem(inventoryItem);


        }

        public double TakeItemFromFridge(string name, double quantity)
        {
            var inventoryItem = _inventoryRepository.Get(name);

            if (inventoryItem == null) {return -1 * quantity;}

            if (inventoryItem.Quantity < quantity) {return inventoryItem.Quantity - quantity;}

            inventoryItem.Quantity -= quantity;
            _inventoryRepository.UpdateInventoryItem(inventoryItem);
            return inventoryItem.Quantity;
        }

    }
}
