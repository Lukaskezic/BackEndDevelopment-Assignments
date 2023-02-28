using System.Text.Json.Serialization;

namespace Assignment4_2.Models
{
    public class MapCard
	{
		public int Id { get; set; }
		public string Class { get; set; }
		public string Cardtype { get; set; }
		public string Set { get; set; }
		public int? SpellSchoolId { get; set; }
		public string Rarity { get; set; }
		public int? Health { get; set; }
		public int? Attack { get; set; }
		public int ManaCost { get; set; }
        public String artistName { get; set; }	
		public String Text { get; set; }
		public String FlavorText { get; set; }
	}
}