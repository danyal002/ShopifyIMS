using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifyIMS.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ShopifyIMS.Dal
{
    public class InventoryRepository
    {
        private readonly IMongoCollection<InventoryItem> _inventoryCollection;

        public InventoryRepository(
            IOptions<InventoryManagementSystemDBContext> InventoryManagementSystemDBContext)
        {
            var mongoClient = new MongoClient(
                InventoryManagementSystemDBContext.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                InventoryManagementSystemDBContext.Value.DatabaseName);

            _inventoryCollection = mongoDatabase.GetCollection<InventoryItem>(
                InventoryManagementSystemDBContext.Value.CollectionName);
        }

        public async Task<List<InventoryItem>> GetAllItemsAsync() =>
            await _inventoryCollection.Find(_ => true).ToListAsync();

        public async Task<InventoryItem?> GetItemByIdAsync(string id) =>
            await _inventoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateItemAsync(InventoryItem newInventoryItem) =>
            await _inventoryCollection.InsertOneAsync(newInventoryItem);

        public async Task UpdateItemAsync(string id, InventoryItem updatedInventoryItem) =>
            await _inventoryCollection.ReplaceOneAsync(x => x.Id == id, updatedInventoryItem);

        public async Task DeleteItemAsync(string id) =>
            await _inventoryCollection.DeleteOneAsync(x => x.Id == id);
    }
}
