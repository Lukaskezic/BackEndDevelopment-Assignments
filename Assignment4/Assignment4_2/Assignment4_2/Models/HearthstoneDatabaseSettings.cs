namespace Assignment4_2.Models
{
    public class HearthstoneDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string[] CollectionName { get; set; } = null!;
    }
}
