using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Base64Image { get; set; }
    }
}