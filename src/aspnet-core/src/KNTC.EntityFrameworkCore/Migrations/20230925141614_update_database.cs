using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "cho_phep_download",
                schema: "KNTC",
                table: "FileAttachments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "luu_tru",
                schema: "KNTC",
                table: "Denounces",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "luu_tru",
                schema: "KNTC",
                table: "Complains",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int[]>(
                name: "managedUnitIds",
                table: "AppUserInfos",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userType",
                table: "AppUserInfos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseMaps",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaseMapCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BaseMapName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    OrderIndex = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMaps", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseMaps",
                schema: "KNTC");

            migrationBuilder.DropColumn(
                name: "cho_phep_download",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "luu_tru",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "luu_tru",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "managedUnitIds",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "userType",
                table: "AppUserInfos");
        }
    }
}
