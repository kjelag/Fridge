using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers
{
    [Route("api/fridge")]
    public class FridgeController : Controller
    {
        private FridgeService _fridgeService;

        public FridgeController()
        {
            _fridgeService = new FridgeService();
        }

        // GET api/values
        [HttpGet]
        public List<InventoryItem> Get()
        {
            return _fridgeService.GetAllInventoryItems();
        }

        // GET api/values/5
        [HttpGet("{name}/{quantity}")]
        public bool IsAvailable(string name, double quantity)
        {
            return _fridgeService.IsItemAvailable(name, quantity);
        }

        // POST api/values
        [HttpPost("{name}/{quantity}")]
        public void AddIngredientToFridge(string name, double quantity)
        {
            _fridgeService.AddIngredientToFridge(new InventoryItem(name, quantity));
        }

        // PUT api/values/5
        [HttpPut("{name}/{quantity}")]
        public void Put(string name, double quantity)
        {
            _fridgeService.AddIngredientToFridge(new InventoryItem(name, quantity));
        }

        // DELETE api/values/5
        [HttpDelete("{name}/{quantity}", Name = "TakeFromFridge")]
        public double Delete(string name, double quantity)
        {
            return _fridgeService.TakeItemFromFridge(name, quantity);
        }
    }
}
