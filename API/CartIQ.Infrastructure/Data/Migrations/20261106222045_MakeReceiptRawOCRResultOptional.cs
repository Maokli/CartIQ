using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartIQ.Infrastructure.CartIQ.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeReceiptRawOCRResultOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_RawOcrResults_RawOcrResultId",
                table: "Receipts");

            migrationBuilder.AlterColumn<Guid>(
                name: "RawOcrResultId",
                table: "Receipts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_RawOcrResults_RawOcrResultId",
                table: "Receipts",
                column: "RawOcrResultId",
                principalTable: "RawOcrResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_RawOcrResults_RawOcrResultId",
                table: "Receipts");

            migrationBuilder.AlterColumn<Guid>(
                name: "RawOcrResultId",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_RawOcrResults_RawOcrResultId",
                table: "Receipts",
                column: "RawOcrResultId",
                principalTable: "RawOcrResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
