using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    public class Paragraph
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
