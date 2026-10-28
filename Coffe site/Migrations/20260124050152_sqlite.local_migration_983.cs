using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffe_site.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_983 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Items_Product_Id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_User_Id",
                table: "Carts");

           
           

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Carts",
                newName: "user_Id");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "Carts",
                newName: "product_Id");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Carts",
                newName: "count");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_User_Id",
                table: "Carts",
                newName: "IX_Carts_user_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_Product_Id",
                table: "Carts",
                newName: "IX_Carts_product_Id");


            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Items",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AlterColumn<int>(
                name: "user_Id",
                table: "Carts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Address",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "Address",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ItemId",
                table: "Orders",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Items_product_Id",
                table: "Carts",
                column: "product_Id",
                principalTable: "Items",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_user_Id",
                table: "Carts",
                column: "user_Id",
                principalTable: "Users",
                principalColumn: "Id");

        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Items_product_Id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_user_Id",
                table: "Carts");

          

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "user_Id",
                table: "Carts",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "product_Id",
                table: "Carts",
                newName: "Product_Id");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "Carts",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Carts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_user_Id",
                table: "Carts",
                newName: "IX_Carts_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_product_Id",
                table: "Carts",
                newName: "IX_Carts_Product_Id");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ItemProduct_Id",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Items",
                type: "BLOB",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

          

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
    }
}
