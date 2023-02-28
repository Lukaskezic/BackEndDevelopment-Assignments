using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Assignment4_2.Models;
using System.Text.Json;

namespace Assignment4_2.Services
{
    public class ClassesService
    {
        private readonly IMongoCollection<Class> _collection;

        public ClassesService(IOptions<HearthstoneDatabaseSettings> dbSettings, MongoDbService service)
        {
            var db = service.Client.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = db.GetCollection<Class>(dbSettings.Value.CollectionName[2]);
        }

        public async Task<IList<Class>> GetClassAll()
        {
            return await _collection.Find(c => true).ToListAsync();
        }

        public async Task<string> GetClass(int? ClassId)
        {
            var classValue = await _collection.Find(Class => Class.Id == ClassId).FirstOrDefaultAsync();
            if (classValue == null)
            {
                return null;
            }
            return classValue.Name;
        }

    }
}
