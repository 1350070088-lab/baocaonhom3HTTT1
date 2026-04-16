using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatingWebb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMatchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserMatches",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "UserMatches");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "UserMatches");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserMatches");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserMatches",
                newName: "MatchedAt");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "UserMatches",
                newName: "User2Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserMatches",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "User1Id",
                table: "UserMatches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMatches_User1Id",
                table: "UserMatches",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatches_User2Id",
                table: "UserMatches",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppUserId",
                table: "Payments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMatches_AppUsers_User1Id",
                table: "UserMatches",
                column: "User1Id",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMatches_AppUsers_User2Id",
                table: "UserMatches",
                column: "User2Id",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMatches_AppUsers_User1Id",
                table: "UserMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMatches_AppUsers_User2Id",
                table: "UserMatches");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_UserMatches_User1Id",
                table: "UserMatches");

            migrationBuilder.DropIndex(
                name: "IX_UserMatches_User2Id",
                table: "UserMatches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserMatches");

            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "UserMatches");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "UserMatches",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "MatchedAt",
                table: "UserMatches",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "UserMatches",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "UserMatches",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserMatches",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
    }
}
