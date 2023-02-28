using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment4_2.Models;
using System.Text.Json;

namespace Assignment4_2.Services
{
    public class RaritiesService
    {
        private readonly IMongoCollection<Rarity> _collection;

        public RaritiesService(IOptions<HearthstoneDatabaseSettings> dbSettings, MongoDbService service)
        {
            var db = service.Client.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = db.GetCollection<Rarity>(dbSettings.Value.CollectionName[3]);
        }

        public async Task<IList<Rarity>> GetRaritiesAll()
        {
            return await _collection.Find(r => true).ToListAsync();
        }

        public async Task<string> GetRarity(int? rarityId)
        {
            var rarityValue = await _collection.Find(Rarity => Rarity.Id == rarityId).FirstOrDefaultAsync();
            if (rarityValue == null)
            {
                return null;
            }
            return rarityValue.Name;
        }
    }
}