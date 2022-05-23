using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifyIMS.Models;
using Microsoft.AspNetCore.Mvc;
using ShopifyIMS.Services;

namespace ShopifyIMS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _service;

        public InventoryController(InventoryService service)
        {
            _service = service;
        }


        [HttpGet("items/all")]
        public async Task<List<InventoryItem>> GetAllItemsAsync()
        {
            return await _service.GetAllItemsAsync();
        }

        [HttpGet("items/warehouse/{warehouse}")]
        public async Task<List<InventoryItem>> GetItemsByWarehouseAsync([FromRoute] string warehouse)
        {
            return await _service.GetItemsByWarehouseAsync(warehouse);
        }

        [HttpGet("items/{itemId}", Name = "GetItemById")]
        public async Task<ActionResult<InventoryItem>> GetItemByIdAsync([FromRoute] string itemId)
        {
            InventoryItem item = await _service.GetItemByIdAsync(itemId);

            if (item is null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost("items/create")]
        public async Task<IActionResult> UpdateItemAsync([FromBody] InventoryItem item)
        {
            await _service.CreateItemAsync(item);

            var createdResource = item;
            var routeValues = new { itemId = createdResource.Id };

            return CreatedAtRoute("GetItemById", routeValues, createdResource);
        }

        [HttpPut("items/update/{itemId}")]
        public async Task<IActionResult> UpdateItemAsync([FromRoute] string itemId, [FromBody] InventoryItem updatedItem)
        {
            var item = await _service.GetItemByIdAsync(itemId);

            if (item is null)
            {
                return NotFound();
            }

            updatedItem.Id = item.Id;

            await _service.UpdateItemAsync(itemId, updatedItem);

            return NoContent();
        }

        [HttpDelete("items/delete/{itemId}")]
        public async Task<IActionResult> DeleteItemAsync(string itemId)
        {
            var item = await _service.GetItemByIdAsync(itemId);

            if (item is null)
            {
                return NotFound();
            }

            await _service.DeleteItemAsync(itemId);
            return NoContent();
        }
    }
}