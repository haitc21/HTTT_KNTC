using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rm_fk_config_unit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Configs_ConfigId",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_ConfigId",
                schema: "KNTC",
                table: "Units");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Units_ConfigId",
                schema: "KNTC",
                table: "Units",
                column: "ConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Configs_ConfigId",
                schema: "KNTC",
                table: "Units",
                column: "ConfigId",
                principalSchema: "KNTC",
                principalTable: "Configs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
