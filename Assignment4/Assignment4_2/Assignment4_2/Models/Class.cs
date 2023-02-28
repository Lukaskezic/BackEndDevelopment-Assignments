using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Assignment4_2.Models
{
    [BsonIgnoreExtraElements]
    public class Class
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Id { get; set; }
		public String? Name { get; set; }
	}
}
