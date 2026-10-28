using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffe_site.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_345 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Items_ItemProduct_Id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ItemProduct_Id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ItemProduct_Id",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Product_Id",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Product_Id",
                table: "Carts",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_User_Id",
                table: "Carts",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Items_Product_Id",
                table: "Carts",
                column: "Product_Id",
                principalTable: "Items",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_User_Id",
                table: "Carts",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Items_Product_Id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_User_Id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Product_Id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_User_Id",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "ItemProduct_Id",
                table: "Carts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Carts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ItemProduct_Id",
                table: "Carts",
                column: "ItemProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Items_ItemProduct_Id",
                table: "Carts",
                column: "ItemProduct_Id",
                principalTable: "Items",
                principalColumn: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
