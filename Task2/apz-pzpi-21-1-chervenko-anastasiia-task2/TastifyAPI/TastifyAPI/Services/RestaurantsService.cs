using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TastifyAPI.Data;
using TastifyAPI.Entities;
using TastifyAPI.Models;

namespace TastifyAPI.Services
{
    public class RestaurantsService
    {
        private readonly IMongoCollection<Restaurant> _restaurantCollection;

         public RestaurantsService(
             IOptions<TastifyDbSettings> tastifyDbSettings)
         {
             var mongoClient = new MongoClient(
                 tastifyDbSettings.Value.ConnectionString);

             var mongoDatabase = mongoClient.GetDatabase(
                 tastifyDbSettings.Value.DatabaseName);

            _restaurantCollection = mongoDatabase.GetCollection<Restaurant>(
                 tastifyDbSettings.Value.RestaurantsCollectionName);
         }

         public async Task<List<Restaurant>> GetAsync() =>
             await _restaurantCollection.Find(_ => true).ToListAsync();

         public async Task<Restaurant?> GetAsync(string id) =>
             await _restaurantCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

         public async Task CreateAsync(Restaurant newRestaurant) =>
             await _restaurantCollection.InsertOneAsync(newRestaurant);

         public async Task UpdateAsync(string id, Restaurant updatedRestaurant) =>
             await _restaurantCollection.ReplaceOneAsync(x => x.Id == id, updatedRestaurant);

         public async Task RemoveAsync(string id) =>
             await _restaurantCollection.DeleteOneAsync(x => x.Id == id);
    }
}

