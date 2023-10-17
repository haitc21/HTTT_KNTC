using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class add_index_units : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Units_ParentId",
                schema: "KNTC",
                table: "Units",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_ParentId",
                schema: "KNTC",
                table: "Units",
                column: "ParentId",
                principalSchema: "KNTC",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_ParentId",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_ParentId",
                schema: "KNTC",
                table: "Units");
        }
    }
}