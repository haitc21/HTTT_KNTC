using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class requiredusertype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.AlterColumn<int>(
                name: "user_type",
                table: "AppUserInfos",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_type",
                table: "AppUserInfos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Denounces",
                columns: new[] { "thoi_gian_tiep_nhan", "ma_ho_so" });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Complains",
                columns: new[] { "thoi_gian_tiep_nhan", "ma_ho_so" });
        }
    }
}
