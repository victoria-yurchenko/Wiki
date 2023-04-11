using Microsoft.EntityFrameworkCore;
using Wiki.Models;

namespace Wiki.Data
{
    public class WikiDb : DbContext
    {
        public WikiDb (DbContextOptions<WikiDb> options)
            : base(options)
        {
        }

        public DbSet<Article> Article { get; set; } = default!;
        public DbSet<ArticleImage> ArticleImage { get; set; } = default!;
        public DbSet<Image> Image { get; set; } = default!;
        public DbSet<Paragraph> Paragraph { get; set; } = default!;
        public DbSet<ArticleParagraph> ArticleParagraph { get; set; } = default!;
    }
}
