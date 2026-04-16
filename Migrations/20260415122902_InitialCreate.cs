using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatingWebb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Bio = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatches", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserMatches",
                columns: new[] { "Id", "Age", "Bio", "ImageUrl", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 24, "", "https://images.unsplash.com/photo-1534528741775-53994a69daeb", "Sarah Thorne", "Đã tương hợp" },
                    { 2, 22, "", "https://images.unsplash.com/photo-1524504388940-b1c1722653e1", "Julianne", "Đã tương hợp" },
                    { 3, 25, "", "https://images.unsplash.com/photo-1531746020798-e7953e3e8c5c", "Emily", "Vừa tương hợp" },
                    { 4, 23, "", "https://images.unsplash.com/photo-1517841905240-472988babdf9", "Chloe", "Đã tương hợp" },
                    { 5, 36, "", "https://vcdn1-giaitri.vnecdn.net/2023/01/18/lee-min-ho-1-1674015655-2365-1674015843.jpg", "Min Ho", "Vừa tương hợp" },
                    { 6, 28, "", "https://i.pinimg.com/originals/8d/3f/0a/8d3f0a1496677f547c6676059d435f11.jpg", "Taehyung", "Đã tương hợp" },
                    { 7, 38, "", "https://i.pinimg.com/736x/8f/3e/2a/8f3e2a738c8247071f0c23946258f4a3.jpg", "Joong Ki", "Đã tương hợp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMatches");
        }
    }
}
