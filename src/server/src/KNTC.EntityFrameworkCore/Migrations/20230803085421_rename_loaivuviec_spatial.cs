using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class renameloaivuviecspatial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoaiVuViec",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                newName: "loai_vu_viec");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "loai_vu_viec",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                newName: "LoaiVuViec");
        }
    }
}