using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppinglistApp.Migrations.ShoppingList
{
    public partial class changesToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shoppinglist",
                columns: table => new
                {
                    ListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoppinglist", x => x.ListID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItem",
                columns: table => new
                {
                    ItemName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShoppinglistListID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItem", x => x.ItemName);
                    table.ForeignKey(
                        name: "FK_ShoppingListItem_Shoppinglist_ShoppinglistListID",
                        column: x => x.ShoppinglistListID,
                        principalTable: "Shoppinglist",
                        principalColumn: "ListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppinglistUser",
                columns: table => new
                {
                    AllowedUsersID = table.Column<int>(type: "int", nullable: false),
                    ShoppinglistsListID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppinglistUser", x => new { x.AllowedUsersID, x.ShoppinglistsListID });
                    table.ForeignKey(
                        name: "FK_ShoppinglistUser_Shoppinglist_ShoppinglistsListID",
                        column: x => x.ShoppinglistsListID,
                        principalTable: "Shoppinglist",
                        principalColumn: "ListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppinglistUser_User_AllowedUsersID",
                        column: x => x.AllowedUsersID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_ShoppinglistListID",
                table: "ShoppingListItem",
                column: "ShoppinglistListID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppinglistUser_ShoppinglistsListID",
                table: "ShoppinglistUser",
                column: "ShoppinglistsListID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListItem");

            migrationBuilder.DropTable(
                name: "ShoppinglistUser");

            migrationBuilder.DropTable(
                name: "Shoppinglist");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
