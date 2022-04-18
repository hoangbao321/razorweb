using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using cs58_Razor_09.Models;

namespace cs58_Razor_09.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titile = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.ID);
                });
            // insert data
            // fake data dùng bogus
            Randomizer.Seed = new Random(8675309);

            var fakearticle = new Faker<Article>();
            fakearticle.RuleFor(a => a.Titile, f => f.Lorem.Sentence(5, 5));
            fakearticle.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2022, 1, 1), new DateTime(2022, 4, 18)));
            fakearticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1, 4));

            for (int i = 0; i < 150; i++)
            {
                Article article = fakearticle.Generate();
                migrationBuilder.InsertData
                    (
                        table: "articles",
                        columns: new[] { "Titile", "Created", "Content" },
                        values: new object[]
                        {
                        article.Titile,
                        article.Created,
                        article.Content
                        }
                    );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
