using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartIQ.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnrichReceiptEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ReceiptItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Barcode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brands = table.Column<string>(type: "TEXT", nullable: false),
                    Categories = table.Column<string>(type: "TEXT", nullable: false),
                    FoodGroup = table.Column<string>(type: "TEXT", nullable: false),
                    Nutrition_EnergyKcalPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_EnergyKjPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_FatPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_SaturatedFatPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_CarbohydratesPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_SugarsPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_ProteinsPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_SaltPer100g = table.Column<double>(type: "REAL", nullable: true),
                    Nutrition_SodiumPer100g = table.Column<double>(type: "REAL", nullable: true),
                    HasNutritionData = table.Column<bool>(type: "INTEGER", nullable: false),
                    NutritionDataPer = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItems_ProductId",
                table: "ReceiptItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                column: "Barcode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptItems_Products_ProductId",
                table: "ReceiptItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptItems_Products_ProductId",
                table: "ReceiptItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptItems_ProductId",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ReceiptItems");
        }
    }
}
