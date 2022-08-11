using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppinglistApp.Migrations
{
    public partial class AddListName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListName",
                table: "SharedLists",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListName",
                table: "SharedLists");
        }
    }
}
