using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wiki.Models
{
    public class ArticleImage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ArticleId))]
        public int ArticleId { get; set; }
        [ForeignKey(nameof(ImageId))]
        public int ImageId { get; set; }

        virtual public List<Article>? Article { get; set; }
        virtual public List<Image>? Image { get; set; }
    }
}
