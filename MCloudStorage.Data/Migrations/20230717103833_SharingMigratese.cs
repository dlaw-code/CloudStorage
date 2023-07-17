using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCloudStorage.Data.Migrations
{
    public partial class SharingMigratese : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_SharedFiles_SharedFileDocumentId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedFiles_Documents_DocumentId",
                table: "SharedFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles");

            migrationBuilder.DropIndex(
                name: "IX_Documents_SharedFileDocumentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SharedFileDocumentId",
                table: "Documents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedFiles_Documents_Id",
                table: "SharedFiles",
                column: "Id",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedFiles_Documents_Id",
                table: "SharedFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles");

            migrationBuilder.AddColumn<int>(
                name: "SharedFileDocumentId",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_SharedFileDocumentId",
                table: "Documents",
                column: "SharedFileDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_SharedFiles_SharedFileDocumentId",
                table: "Documents",
                column: "SharedFileDocumentId",
                principalTable: "SharedFiles",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedFiles_Documents_DocumentId",
                table: "SharedFiles",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
