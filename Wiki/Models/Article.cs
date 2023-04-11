using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }
}
