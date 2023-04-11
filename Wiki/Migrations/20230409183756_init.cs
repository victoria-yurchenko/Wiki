using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiki.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleParagraph",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    ParagraphId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleParagraph", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_ArticleImage_ArticleImageId",
                        column: x => x.ArticleImageId,
                        principalTable: "ArticleImage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleImageId = table.Column<int>(type: "int", nullable: true),
                    ArticleParagraphId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_ArticleImage_ArticleImageId",
                        column: x => x.ArticleImageId,
                        principalTable: "ArticleImage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Article_ArticleParagraph_ArticleParagraphId",
                        column: x => x.ArticleParagraphId,
                        principalTable: "ArticleParagraph",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Paragraph",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleParagraphId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paragraph", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paragraph_ArticleParagraph_ArticleParagraphId",
                        column: x => x.ArticleParagraphId,
                        principalTable: "ArticleParagraph",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleImageId",
                table: "Article",
                column: "ArticleImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleParagraphId",
                table: "Article",
                column: "ArticleParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ArticleImageId",
                table: "Image",
                column: "ArticleImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Paragraph_ArticleParagraphId",
                table: "Paragraph",
                column: "ArticleParagraphId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Paragraph");

            migrationBuilder.DropTable(
                name: "ArticleImage");

            migrationBuilder.DropTable(
                name: "ArticleParagraph");
        }
    }
}
