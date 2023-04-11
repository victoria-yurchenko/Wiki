using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wiki.Models
{
    public class ArticleParagraph
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ArticleId))]
        public int ArticleId { get; set; }
        [ForeignKey(nameof(ParagraphId))]
        public int ParagraphId { get; set; }

        virtual public List<Article>? Article { get; set; }
        virtual public List<Paragraph>? Paragraph { get; set; }
    }
}
