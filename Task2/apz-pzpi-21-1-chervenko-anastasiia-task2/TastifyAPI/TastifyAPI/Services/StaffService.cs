﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TastifyAPI.Data;
using TastifyAPI.Entities;
using TastifyAPI.Models;

namespace TastifyAPI.Services
{
    public class StaffService
    {
        private readonly IMongoCollection<Staff> _staffCollection;

        public StaffService(
            IOptions<TastifyDbSettings> tastifyDbSettings)
        {
            var mongoClient = new MongoClient(
                tastifyDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tastifyDbSettings.Value.DatabaseName);

            _staffCollection = mongoDatabase.GetCollection<Staff>(
                tastifyDbSettings.Value.StaffCollectionName);
        }

        public async Task<List<Staff>> GetAsync() =>
            await _staffCollection.Find(_ => true).ToListAsync();

        public async Task<Staff?> GetAsync(string id) =>
            await _staffCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Staff newStaff) =>
            await _staffCollection.InsertOneAsync(newStaff);

        public async Task UpdateAsync(string id, Staff updatedStaff) =>
            await _staffCollection.ReplaceOneAsync(x => x.Id == id, updatedStaff);

        public async Task RemoveAsync(string id) =>
            await _staffCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<Staff> GetByLoginAsync(string login)
        {
            return await _staffCollection.Find(x => x.Login == login).FirstOrDefaultAsync();
        }

    }
}
