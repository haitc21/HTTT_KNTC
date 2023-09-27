using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class renameusertype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userType",
                table: "AppUserInfos",
                newName: "user_type");

            migrationBuilder.RenameColumn(
                name: "managedUnitIds",
                table: "AppUserInfos",
                newName: "managed_unit_ids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "AppUserInfos",
                newName: "userType");

            migrationBuilder.RenameColumn(
                name: "managed_unit_ids",
                table: "AppUserInfos",
                newName: "managedUnitIds");
        }
    }
}
