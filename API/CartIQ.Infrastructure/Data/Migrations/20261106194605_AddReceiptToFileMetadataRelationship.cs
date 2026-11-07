using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartIQ.Infrastructure.CartIQ.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptToFileMetadataRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileMetadataId",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_FileMetadataId",
                table: "Receipts",
                column: "FileMetadataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_FileMetadatas_FileMetadataId",
                table: "Receipts",
                column: "FileMetadataId",
                principalTable: "FileMetadatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_FileMetadatas_FileMetadataId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_FileMetadataId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "FileMetadataId",
                table: "Receipts");
        }
    }
}
