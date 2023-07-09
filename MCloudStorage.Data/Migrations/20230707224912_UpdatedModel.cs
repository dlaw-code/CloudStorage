using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MCloudStorage.Data.Migrations
{
    public partial class UpdatedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileProperties");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileReference",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileStatus",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Filelink",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileReference",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileStatus",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Filelink",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Documents");

            migrationBuilder.CreateTable(
                name: "FileProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FileReference = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    FileStatus = table.Column<int>(type: "integer", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: true),
                    FileUploadType = table.Column<string>(type: "text", nullable: true),
                    Filelink = table.Column<string>(type: "text", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProperties", x => x.Id);
                });
        }
    }
}
