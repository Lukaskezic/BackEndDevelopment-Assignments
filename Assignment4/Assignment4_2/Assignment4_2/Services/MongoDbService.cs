using MongoDB.Driver;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Assignment4_2.Models
{
    public class MongoDbService
    {

        private readonly MongoClient _client;

        public MongoDbService(IOptions<HearthstoneDatabaseSettings> dbSettings)
        {
           _client = new MongoClient(dbSettings.Value.ConnectionString);

            var db = _client.GetDatabase(dbSettings.Value.DatabaseName);

            //Card

            var CardList = db.ListCollectionNames().ToList().Contains("Card");
            var CardCollection = db.GetCollection<Card>(dbSettings.Value.CollectionName[0]);

            if (!CardList)
            {
                foreach (var path in new[] { "cards.json" })
                {
                    using (var file = new StreamReader(path))
                    {
                        var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        CardCollection.InsertMany(cards);
                    }
                }
            }

            //Cardtype

            var CardTypeList = db.ListCollectionNames().ToList().Contains("Cardtype");
            var CardTypeCollection = db.GetCollection<CardType>(dbSettings.Value.CollectionName[1]);

            if (!CardTypeList)
            {
                foreach (var path in new[] { "types.json" })
                {
                    using (var file = new StreamReader(path))
                    {
                        var cardType = JsonSerializer.Deserialize<List<CardType>>(file.ReadToEnd(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        CardTypeCollection.InsertMany(cardType);
                    }
                }
            }

            //Class

            var ClassList = db.ListCollectionNames().ToList().Contains("Class");
            var ClassCollection = db.GetCollection<Class>(dbSettings.Value.CollectionName[2]);

            if (!ClassList)
            {
                foreach (var path in new[] { "classes.json" })
                {
                    using (var file = new StreamReader(path))
                    {
                        var classes = JsonSerializer.Deserialize<List<Class>>(file.ReadToEnd(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        ClassCollection.InsertMany(classes);
                    }
                }
            }

            //Rarity

            var RarityList = db.ListCollectionNames().ToList().Contains("Rarity");
            var RarityCollection = db.GetCollection<Rarity>(dbSettings.Value.CollectionName[3]);
            if (!RarityList)
            {
                foreach (var path in new[] { "rarities.json" })
                {
                    using (var file = new StreamReader(path))
                    {
                        var rarity = JsonSerializer.Deserialize<List<Rarity>>(file.ReadToEnd(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        RarityCollection.InsertMany(rarity);
                    }
                }
            }

            //Set

            var CollectionList = db.ListCollectionNames().ToList().Contains("Set");
            var SetList = db.GetCollection<Set>(dbSettings.Value.CollectionName[4]);

            if (!CollectionList)
            {
                foreach (var path in new[] { "sets.json" })
                {
                    using (var file = new StreamReader(path))
                    {
                        var sets = JsonSerializer.Deserialize<List<Set>>(file.ReadToEnd(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        SetList.InsertMany(sets);
                    }
                }
            }
        }

        public MongoClient Client
        {
            get
            {
                return _client;
            }
        }
    }
}

