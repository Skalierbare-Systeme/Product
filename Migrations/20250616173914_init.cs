using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace postsPraktikum.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PostTitle = table.Column<string>(type: "longtext", nullable: false),
                    PostDescription = table.Column<string>(type: "longtext", nullable: true),
                    PostText = table.Column<string>(type: "longtext", nullable: false),
                    PostCreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Photos = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
