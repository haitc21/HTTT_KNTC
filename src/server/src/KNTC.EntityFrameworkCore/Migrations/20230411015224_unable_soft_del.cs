using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class unable_soft_del : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "UnitTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "UnitTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "UnitTypes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "LandTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "LandTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "LandTypes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Complains");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "UnitTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "UnitTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "UnitTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "Units",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Units",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "LandTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "LandTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "LandTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "FileAttachments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "FileAttachments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "DocumentTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "DocumentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "DocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "Denounces",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Denounces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "Configs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Configs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Configs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "Complains",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "Complains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "Complains",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
