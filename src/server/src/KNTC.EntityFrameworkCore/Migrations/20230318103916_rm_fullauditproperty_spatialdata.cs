using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rm_fullauditproperty_spatialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                schema: "KNTC",
                table: "SpatialData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "SpatialData",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "KNTC",
                table: "SpatialData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "KNTC",
                table: "SpatialData",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "KNTC",
                table: "SpatialData",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "KNTC",
                table: "SpatialData",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "SpatialData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "KNTC",
                table: "SpatialData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "KNTC",
                table: "SpatialData",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                schema: "KNTC",
                table: "SpatialData",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
