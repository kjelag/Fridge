using System;
using System.Security.Cryptography.X509Certificates;
using Fridge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FridgeUnitTest
{
    [TestClass]
    public class FridgeTests
    {
        [TestClass]
        public class WithEmptyFridge
        {
            private string inventoryName = "Meatballs";

            [TestMethod]
            public void GetItem()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var result = fridge.GetInventoryItem("Gorgonzola");
                Assert.AreEqual(null, result);
            }

            [TestMethod]
            public void ListAllInventoryItems()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var result = fridge.GetAllInventoryItems();
                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void AddOneInventoryItem()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);
                var inventoryItem = new InventoryItem(inventoryName, 10);
                fridge.AddIngredientToFridge(inventoryItem);

                Assert.AreEqual(1, fakeInventoryRepository.InventoryItems.Count);
                Assert.AreEqual(inventoryName, fakeInventoryRepository.InventoryItems[0].Name);
                Assert.AreEqual(10, fakeInventoryRepository.InventoryItems[0].Quantity);
            }

            [TestMethod]
            public void AddTwoIdenticalInventoryItem()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var inventoryItem = new InventoryItem(inventoryName, 10);
                fridge.AddIngredientToFridge(inventoryItem);
                fridge.AddIngredientToFridge(inventoryItem);

                Assert.AreEqual(1, fakeInventoryRepository.InventoryItems.Count);
                Assert.AreEqual(inventoryName, fakeInventoryRepository.InventoryItems[0].Name);
                Assert.AreEqual(20, fakeInventoryRepository.InventoryItems[0].Quantity);
            }

            [TestMethod]
            public void IsItemAvailable()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);
                Assert.AreEqual(false, fridge.IsItemAvailable(inventoryName, 7));
            }

            [TestMethod]
            public void RemoveInventoryItem()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);
                Assert.AreEqual(-5, fridge.TakeItemFromFridge(inventoryName, 5));
            }

        }

        [TestClass]
        public class WithNonEmptyFridge
        {
            private string inventoryName = "Meatballs";

            [TestMethod]
            public void GetExistingItem()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var inventoryItem = new InventoryItem(inventoryName, 10);
                fridge.AddIngredientToFridge(inventoryItem);

                var result = fridge.GetInventoryItem(inventoryName);
                Assert.AreEqual(inventoryName, result.Name);
                Assert.AreEqual(10, result.Quantity);
            }


            [TestMethod]
            public void GetNonExistingItem()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var inventoryItem = new InventoryItem(inventoryName, 10);
                fridge.AddIngredientToFridge(inventoryItem);

                var result = fridge.GetInventoryItem("Gorgonzola");
                Assert.AreEqual(null, result);
            }

            [TestMethod]
            public void ListAllInventoryItems()
            {
                var invItem1 = "Meatballs";
                var invItem2 = "Potato";
                var invItem3 = "Pasta";

                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var inventoryItem = new InventoryItem(invItem1, 10);
                fridge.AddIngredientToFridge(inventoryItem);
                inventoryItem = new InventoryItem(invItem2, 50);
                fridge.AddIngredientToFridge(inventoryItem);
                inventoryItem = new InventoryItem(invItem3, 4);
                fridge.AddIngredientToFridge(inventoryItem);

                var result = fridge.GetAllInventoryItems();
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual(invItem1, result[0].Name);
                Assert.AreEqual(invItem2, result[1].Name);
                Assert.AreEqual(invItem3, result[2].Name);
            }

            [TestMethod]
            public void IsItemAvailable()
            {
                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);
                var inventoryItem = new InventoryItem(inventoryName, 10);
                fridge.AddIngredientToFridge(inventoryItem);
                Assert.AreEqual(true, fridge.IsItemAvailable(inventoryName, 7));
            }

            [TestMethod]
            public void RemoveExistingInventoryItem()
            {
                var invItem1 = "Meatballs";
                var invItem2 = "Potato";
                var invItem3 = "Pasta";

                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var inventoryItem = new InventoryItem(invItem1, 10);
                fridge.AddIngredientToFridge(inventoryItem);
                inventoryItem = new InventoryItem(invItem2, 50);
                fridge.AddIngredientToFridge(inventoryItem);
                inventoryItem = new InventoryItem(invItem3, 4);
                fridge.AddIngredientToFridge(inventoryItem);

                Assert.AreEqual(20, fridge.TakeItemFromFridge(invItem2, 30));
                Assert.AreEqual(-10, fridge.TakeItemFromFridge(invItem2, 30));
            }

            [TestMethod]
            public void RemoveNonExistingInventoryItem()
            {
                var invItem1 = "Meatballs";
                var invItem2 = "Potato";
                var invRemoveItem = "Sauce";

                var fakeInventoryRepository = new FakeInventoryRepository();
                var fridge = new FridgeService(fakeInventoryRepository);

                var inventoryItem = new InventoryItem(invItem1, 10);
                fridge.AddIngredientToFridge(inventoryItem);
                inventoryItem = new InventoryItem(invItem2, 50);
                fridge.AddIngredientToFridge(inventoryItem);

                Assert.AreEqual(-5, fridge.TakeItemFromFridge(invRemoveItem, 5));
            }

        }
    }
}
