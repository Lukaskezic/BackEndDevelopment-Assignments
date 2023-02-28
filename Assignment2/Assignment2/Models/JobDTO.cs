using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class JobDTO
    {
        public long JobId { get; set; }
        public string? Customer { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public List<Model>? Models { get; set; }
    }
}
