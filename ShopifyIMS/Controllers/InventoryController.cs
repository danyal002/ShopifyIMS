using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifyIMS.Dal;
using ShopifyIMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopifyIMS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryRepository _inventoryRepository;

        public InventoryController(InventoryRepository InventoryRepository) =>
            _inventoryRepository = InventoryRepository;

        [HttpGet]
        public async Task<List<InventoryItem>> Get() =>
            await _inventoryRepository.GetAllItemsAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<InventoryItem>> Get(string id)
        {
            var InventoryItem = await _inventoryRepository.GetItemByIdAsync(id);

            if (InventoryItem is null)
            {
                return NotFound();
            }

            return InventoryItem;
        }

        [HttpPost]
        public async Task<IActionResult> Post(InventoryItem newBook)
        {
            await _inventoryRepository.CreateItemAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, InventoryItem updatedBook)
        {
            var InventoryItem = await _inventoryRepository.GetItemByIdAsync(id);

            if (InventoryItem is null)
            {
                return NotFound();
            }

            updatedBook.Id = InventoryItem.Id;

            await _inventoryRepository.UpdateItemAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var InventoryItem = await _inventoryRepository.GetItemByIdAsync(id);

            if (InventoryItem is null)
            {
                return NotFound();
            }

            await _inventoryRepository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}