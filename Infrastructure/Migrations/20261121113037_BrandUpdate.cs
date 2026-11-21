using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261121113037_BrandUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "ProductBrands",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "ProductCategoryId",
            table: "ProductBrands",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_ProductBrands_ProductCategoryId",
            table: "ProductBrands",
            column: "ProductCategoryId");

        migrationBuilder.AddForeignKey(
            name: "FK_ProductBrands_ProductCategories_ProductCategoryId",
            table: "ProductBrands",
            column: "ProductCategoryId",
            principalTable: "ProductCategories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_ProductBrands_ProductCategories_ProductCategoryId",
            table: "ProductBrands");

        migrationBuilder.DropIndex(
            name: "IX_ProductBrands_ProductCategoryId",
            table: "ProductBrands");

        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "ProductBrands");

        migrationBuilder.DropColumn(
            name: "ProductCategoryId",
            table: "ProductBrands");
    }
}
