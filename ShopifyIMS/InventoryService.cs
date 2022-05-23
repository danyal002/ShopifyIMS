using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
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

        public async Task<List<InventoryItem>> GetItemsByWarehouseAsync(string warehouse)
        {
            List<InventoryItem> allItems = await _repository.GetAllItemsAsync();

            return allItems.Where(item => item.StorageLocations.Keys.Contains(warehouse)).ToList();
        }

        public async Task<InventoryItem?> GetItemByIdAsync(string id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public async Task CreateItemAsync(InventoryItem newInventoryItem)
        {
            ValidateWarehouseTotal(newInventoryItem);
            newInventoryItem.Id = ObjectId.GenerateNewId().ToString();
            await _repository.CreateItemAsync(newInventoryItem);
        }

        public async Task UpdateItemAsync(string id, InventoryItem updatedInventoryItem)
        {
            ValidateWarehouseTotal(updatedInventoryItem);
            await _repository.UpdateItemAsync(id, updatedInventoryItem);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _repository.DeleteItemAsync(id);
        }

        private void ValidateWarehouseTotal(InventoryItem item)
        {
            int warehouseTotal = 0;
            foreach(var warehouse in item.StorageLocations.Keys)
            {
                warehouseTotal += item.StorageLocations[warehouse];
            }

            // we're assuming that all items have to be stored somewhere. This can be easily changed according to business requirements.
            if(warehouseTotal != item.QtyInStock)
            {
                throw new ArgumentException($"Quantity in stock must match sum of warehouse quanities for {item.Name}");
            }
        }
    }
}
