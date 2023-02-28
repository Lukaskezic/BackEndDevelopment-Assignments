using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Assignment4_2.Models
{
	[BsonIgnoreExtraElements]
	public class Card
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Id { get; set; }
		public int ClassId { get; set; }
		public int cardTypeId { get; set; }
        public int cardSetId { get; set; }
		public int? SpellSchoolId { get; set; }
		public int RarityId { get; set; }
		public int? Health { get; set; }
		public int? Attack { get; set; }
		public int ManaCost { get; set; }
		public string artistName { get; set; }
        public String Text { get; set; }
		public String FlavorText { get; set; }
    }
}
