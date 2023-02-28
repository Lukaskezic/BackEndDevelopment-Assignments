using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment4_2.Models;
using System.Text.Json;

namespace Assignment4_2.Services
{
    public class SetsService
    {
        private readonly IMongoCollection<Set> _collection;

        public SetsService(IOptions<HearthstoneDatabaseSettings> dbSettings, MongoDbService service)
        {
            var db = service.Client.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = db.GetCollection<Set>(dbSettings.Value.CollectionName[4]);
        }

        public async Task<IList<Set>> GetSets()
        {
            return await _collection.Find(Set => true).ToListAsync();
        }

        public async Task<string> GetSet(int? cardSetId)
        {
            var setValue = await _collection.Find(Set => Set.Id == cardSetId).FirstOrDefaultAsync();
            if (setValue == null)
            {
                return null;
            }
            return setValue.Name;
        }
    }
}