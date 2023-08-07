using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCloudStorage.Data.Migrations
{
    public partial class SharedFileCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedFileId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SharedFiles",
                newName: "SenderUserId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverUserId",
                table: "SharedFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "Documents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SharedFiles_DocumentId",
                table: "SharedFiles",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedFiles_Documents_DocumentId",
                table: "SharedFiles",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedFiles_Documents_DocumentId",
                table: "SharedFiles");

            migrationBuilder.DropIndex(
                name: "IX_SharedFiles_DocumentId",
                table: "SharedFiles");

            migrationBuilder.DropColumn(
                name: "ReceiverUserId",
                table: "SharedFiles");

            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "SenderUserId",
                table: "SharedFiles",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "SharedFileId",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
