using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartIQ.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptRawOcrResultForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_RawOcrResults_RawOcrResultId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_RawOcrResultId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "RawOcrResultId",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_RawOcrResults_Receipts_Id",
                table: "RawOcrResults",
                column: "Id",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawOcrResults_Receipts_Id",
                table: "RawOcrResults");

            migrationBuilder.AddColumn<Guid>(
                name: "RawOcrResultId",
                table: "Receipts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_RawOcrResultId",
                table: "Receipts",
                column: "RawOcrResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_RawOcrResults_RawOcrResultId",
                table: "Receipts",
                column: "RawOcrResultId",
                principalTable: "RawOcrResults",
                principalColumn: "Id");
        }
    }
}
