using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment4_2.Models;
using MongoDB.Bson;
using System.Text.Json;

namespace Assignment4_2.Services
{
    public class CardsService
    {
        private readonly IMongoCollection<Card> _collection;

        private Pagination cardpagination = new Pagination();
        public CardsService(
            IOptions<HearthstoneDatabaseSettings> dbSettings, MongoDbService service)
        {
            var db = service.Client.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = db.GetCollection<Card>(dbSettings.Value.CollectionName[0]);
        }

        public async Task<IList<Card>> GetCard(int? page = 1, int? setid = null, string? artistName = null, int? classid = null, int? rarityid = null)
        {
            var builder = Builders<Card>.Filter;
            var filter = builder.Empty;

            if (setid != null)
            {
                filter &= builder.Eq(x => x.cardSetId, setid);
            }

            if (artistName?.Length > 0)
            {
                filter &= builder.Regex(x => x.artistName, new BsonRegularExpression($"/{artistName}/i"));
            }

            if (classid != null)
            {
                filter &= builder.Eq(x => x.ClassId, classid);
            }

            if (rarityid != null)
            {
                filter &= builder.Eq(x => x.RarityId, rarityid);
            }

            return await _collection.Find(filter).Skip((page - 1) * cardpagination.PageSize).Limit(cardpagination.PageSize).ToListAsync();
        }
    }
}
