using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBase.Migrations
{
    public partial class alter_userinfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppUserInfos",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppUserInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppUserInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppUserInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppUserInfos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppUserInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppUserInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppUserInfos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppUserInfos",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppUserInfos");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppUserInfos");
        }
    }
}
