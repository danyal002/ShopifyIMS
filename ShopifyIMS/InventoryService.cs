using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifyIMS.Dal;
using ShopifyIMS.Models;

namespace ShopifyIMS.Services
{
    public class InventoryService
    {
        private InventoryRepository _repository;

        public InventoryService(InventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task<InventoryItem?> GetItemByIdAsync(string id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public async Task CreateItemAsync(InventoryItem newInventoryItem)
        {
            await _repository.CreateItemAsync(newInventoryItem);
        }

        public async Task UpdateItemAsync(string id, InventoryItem updatedInventoryItem)
        {
            await _repository.UpdateItemAsync(id, updatedInventoryItem);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _repository.DeleteItemAsync(id);
        }
    }
}
