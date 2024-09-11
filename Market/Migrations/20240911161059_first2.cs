using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class first2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_discountid",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlackedListTokens",
                table: "BlackedListTokens");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "discounts");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "BlackedListTokens",
                newName: "blacked_list_tokens");

            migrationBuilder.RenameIndex(
                name: "IX_Products_discountid",
                table: "products",
                newName: "IX_products_discountid");

            migrationBuilder.RenameIndex(
                name: "IX_Products_category_id",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_discounts",
                table: "discounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blacked_list_tokens",
                table: "blacked_list_tokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_discountid",
                table: "products",
                column: "discountid",
                principalTable: "discounts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_category_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_discountid",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_discounts",
                table: "discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blacked_list_tokens",
                table: "blacked_list_tokens");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "discounts",
                newName: "Discounts");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "blacked_list_tokens",
                newName: "BlackedListTokens");

            migrationBuilder.RenameIndex(
                name: "IX_products_discountid",
                table: "Products",
                newName: "IX_Products_discountid");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "Products",
                newName: "IX_Products_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlackedListTokens",
                table: "BlackedListTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_discountid",
                table: "Products",
                column: "discountid",
                principalTable: "Discounts",
                principalColumn: "id");
        }
    }
}
