using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment4_2.Models;
using System.Text.Json;

namespace Assignment4_2.Services
{
    public class CardtypesService
    {
        private readonly IMongoCollection<CardType> _collection;

        public CardtypesService(IOptions<HearthstoneDatabaseSettings> dbSettings, MongoDbService service)
        {
            var db = service.Client.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = db.GetCollection<CardType>( dbSettings.Value.CollectionName[1]);
        }

        public async Task<IList<CardType>> GetCardTypeAll()
        {
            return await _collection.Find(c => true).ToListAsync();
        }

        public async Task<string> GetCardType(int? cardTypeId)
        {
            var typeValue = await _collection.Find(Type => Type.Id == cardTypeId).FirstOrDefaultAsync();
            if (typeValue == null)
            {
                return null;
            }
            return typeValue.Name;
        }
    }
}
