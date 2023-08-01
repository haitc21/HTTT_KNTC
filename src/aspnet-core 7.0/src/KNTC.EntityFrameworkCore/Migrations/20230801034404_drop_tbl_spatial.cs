using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class droptblspatial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpatialDatas",
                schema: "SPATIAL_DATA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SPATIAL_DATA");

            migrationBuilder.CreateTable(
                name: "SpatialDatas",
                schema: "SPATIAL_DATA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cccdcmnd = table.Column<string>(name: "cccd_cmnd", type: "character varying(50)", maxLength: 50, nullable: false),
                    congkhai = table.Column<bool>(name: "cong_khai", type: "boolean", nullable: false, defaultValue: false),
                    dienthoai = table.Column<string>(name: "dien_thoai", type: "character varying(50)", maxLength: 50, nullable: false),
                    DuLieuHinhHoc = table.Column<Geometry>(type: "geometry", nullable: true),
                    DuLieuToaDo = table.Column<Point>(type: "geometry", nullable: true),
                    idhoso = table.Column<Guid>(name: "id_ho_so", type: "uuid", nullable: false),
                    ketqua = table.Column<int>(name: "ket_qua", type: "integer", nullable: true),
                    linhvuc = table.Column<int>(name: "linh_vuc", type: "integer", nullable: false),
                    LoaiVuViec = table.Column<int>(type: "integer", nullable: false),
                    mahoso = table.Column<string>(name: "ma_ho_so", type: "character varying(50)", maxLength: 50, nullable: false),
                    maquanhuyen = table.Column<int>(name: "ma_quan_huyen", type: "integer", nullable: false),
                    matinhtp = table.Column<int>(name: "ma_tinh_tp", type: "integer", nullable: false),
                    maxaphuongtt = table.Column<int>(name: "ma_xa_phuong_tt", type: "integer", nullable: false),
                    nguoinopdon = table.Column<string>(name: "nguoi_nop_don", type: "character varying(100)", maxLength: 100, nullable: false),
                    thoigiantiepnhan = table.Column<DateTime>(name: "thoi_gian_tiep_nhan", type: "timestamp without time zone", nullable: false),
                    tieude = table.Column<string>(name: "tieu_de", type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpatialDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpatialDatas_id_ho_so",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                column: "id_ho_so");
        }
    }
}